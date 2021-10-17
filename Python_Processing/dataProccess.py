import os
import numpy as np
import math
import matplotlib.pyplot as plt

#Get a list with all the files in the directory
path = "../DataLogging/JointData"
list = os.listdir(path)

nLines = 0

data1 = np.zeros((42,3))
data2 = np.zeros((42,3))

speed = np.zeros((1,42))
acc = np.zeros((1,42))
aux = np.zeros((1,42))
check = 0

times = np.zeros(1)

for file in list:
    print(file)

    fileDirectory = path
    fileDirectory += "/{}".format(file)

    f = open(fileDirectory, "r")

    line1 = f.readline()
    line1 = f.readline()
    line2 = f.readline()
    line3 = f.readline()
    nLines = 1
    while line3 != '':
        nLines += 1
        #Separate the coordinates from each joint
        x = line1.split(";")
        i = 0
        #for coor in x:
        for i in range(42):
            y = x[i].split(",")
            data1[i][0] = float(y[0])
            data1[i][1] = float(y[1])
            data1[i][2] = float(y[2])
            #i += 1
        time1 = float(x[42])
        print(time1)
        times = np.append(times,time1)

        x2 = line2.split(";")
        i = 0
        #for coor in x2:
        for i in range(42):
            y = x2[i].split(",")
            data2[i][0] = float(y[0])
            data2[i][1] = float(y[1])
            data2[i][2] = float(y[2])
            #i += 1
        time2 = float(x2[42])

        #Calculate the speed
        if check == 0:
            for i in range(42):
                speed[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
            check = 1
        else:
            for i in range(42):
                aux[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
            speed = np.vstack([speed,aux])

        line1 = line2
        line2 = line3
        line3 = f.readline()
    print(time2)
    times = np.append(times,time2)
    print("Number of line in the file: ",nLines)
    print(speed.shape)

    check = 0

    for i in range(21):
        plt.figure(1)
        plt.plot( times[1:(nLines)], speed[:, i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        plt.ylabel('Velocity')
        plt.xlabel('Time')
        plt.title("Right Hand")
        plt.legend()

        plt.figure(2)
        plt.plot( times[1:(nLines)], speed[:, 21+i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        plt.ylabel('Velocity')
        plt.xlabel('Time')
        plt.title("Left Hand")
        plt.legend()

    #Calculate the acceleration
    iiii = np.size(speed, axis=0)
    for i in range(np.size(speed, axis=0)-2):
        for j in range(42):
            if check < 42 :
                acc[i][j] =  (speed[i+1][j]-speed[i][j])/(times[i+2]+times[i+1])
                check += 1
            else:
                aux[0][j] = (speed[i+1][j]-speed[i][j])/(times[i+2]+times[i+1])
        acc = np.vstack([acc,aux])

    
    for i in range(21):
        plt.figure(3)
        plt.plot( acc[:, i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        plt.ylabel('Acceleration')
        plt.title("Right Hand")
        plt.legend()

        plt.figure(4)
        plt.plot( acc[:, 21+i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        plt.ylabel('Acceleration')
        plt.title("Left Hand")
        plt.legend()
                
    #Jitter
    jitter = np.zeros(np.size(acc, axis=0)-1)
    for i in range(np.size(acc, axis=0)-1):
        for j in range(42):
            if (acc[i][j]*acc[i+1][j] < 0):
                jitter[i] += 1
            else:
                if(jitter[i]!=0):
                    jitter[i] += -1
    plt.figure(5)
    plt.plot( jitter,marker='o',linestyle='dashed', linewidth=2, markersize=10, label='Jitter')
    plt.ylabel("Jitter")
    
    plt.show()
    f.close()














    #while True:
    #    line = f.readline()
    #    if line == '':
    #        print("Number of line in the file: ",nLines)
    #        nLines = 0
    #        break
    #    nLines += 1
