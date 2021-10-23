import os
import numpy as np
import math
import matplotlib.pyplot as plt

#Get a list with all the files in the directory
path = "../DataLogging/JointData"
path1 = "../DataLogging/VelocityJoint"
if os.path.exists(path1) == False:
    os.mkdir(path1)
list = os.listdir(path)

nLines = 0

first = 1

data1 = np.zeros((42,3))
data2 = np.zeros((42,3))

#speed = np.zeros((1,42))
acc = np.zeros((1,42))
aux = np.zeros((1,43))
check = 0

times = np.zeros(1)

for file in list:
    print(file.replace('.csv', '') + '_Speed.csv')


    fileDirectory2 = path1 + "/{}".format(file.replace('.csv', '') + '_Speed.csv')
    fileDirectory = path
    fileDirectory += "/{}".format(file)

    speed = np.zeros((1,43))

    f = open(fileDirectory, "r")


    #line1 = f.readline()
    line1 = f.readline()
    line1 = f.readline()
    line2 = f.readline()
    #line3 = f.readline()
    nLines = 1
    #while line3 != '':
    while line2 != '':
        nLines += 1
        #Separate the coordinates from each joint
        x = line1.split(",")
        i = 0
        #for coor in x:
        for i in range(42):
            y = x[i].split(" ")
            data1[i][0] = float(y[0])
            data1[i][1] = float(y[1])
            data1[i][2] = float(y[2])
            #i += 1
        time1 = float(x[42])
        print(time1)
        times = np.append(times,time1)

        x2 = line2.split(",")
        i = 0
        #for coor in x2:
        for i in range(42):
            y = x2[i].split(" ")
            data2[i][0] = float(y[0])
            data2[i][1] = float(y[1])
            data2[i][2] = float(y[2])
            #i += 1
        time2 = float(x2[42])

        #Calculate the speed CHANMGE HGERE
        #if check == 0:
            #for i in range(42):
                #speed[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
            #speed[0][42] = time2
            #check = 1
        #else:
        ####for i in range(42):
            ####aux[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
        ####aux[0][42] = time2
        if first == 1:
            for i in range(42):
                speed[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
            speed[0][42] = time2
            first = 0
        else:
            for i in range(42):
                aux[0][i] = (math.sqrt( (data2[i][0] - data1[i][0])**2 + (data2[i][1] - data1[i][1])**2 + (data2[i][2] - data1[i][2])**2  )) / (time2 - time1)
            aux[0][42] = time2
            speed = np.vstack([speed,aux])

        line1 = line2
        line2 = f.readline()
        #line2 = line3
        #line3 = f.readline()
    print(time2)
    times = np.append(times,time2)
    print("Number of line in the file: ",nLines)
    print(speed.shape)
    print(np.size(speed, 0))

    check = 0

    f.close()

    f2 = open(fileDirectory2, "w")
    #Header
    line = 'WRIST,THUMB_CMC,THUMB_MCP,THUMB_IP,THUMB_TIP,INDEX_FINGER_MCP,INDEX_FINGER_PIP,INDEX_FINGER_DIP,INDEX_FINGER_TIP,MIDDLE_FINGER_MCP,MIDDLE_FINGER_PIP,MIDDLE_FINGER_DIP,MIDDLE_FINGER_TIP, RING_FINGER_MCP,RING_FINGER_PIP,RING_FINGER_DIP,RING_FINGER_TIP,PINKY_MCP,PINKY_PIP,PINKY_DIP,PINKY_TIP,WRIST2,THUMB_CMC2,THUMB_MCP2,THUMB_IP2,THUMB_TIP2,INDEX_FINGER_MCP2,INDEX_FINGER_PIP2,INDEX_FINGER_DIP2,INDEX_FINGER_TIP2,MIDDLE_FINGER_MCP2,MIDDLE_FINGER_PIP2,MIDDLE_FINGER_DIP2,MIDDLE_FINGER_TIP2,RING_FINGER_MCP2,RING_FINGER_PIP2,RING_FINGER_DIP2,RING_FINGER_TIP2,PINKY_MCP2,PINKY_PIP2,PINKY_DIP2,PINKY_TIP2,Timestamp\n'
    f2.write(line)
    line = ''
    for i in range(np.size(speed, 0)):
        #Write in the file the info about the speed
        line = str(speed[i][0])
        for j in range(1,43):
            line += (',' + str(speed[i][j]))
        line += '\n'
        f2.write(line)
    f2.close()



    #for i in range(21):
        #plt.figure(1)
        #plt.plot( times[1:(nLines)], speed[:, i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        #plt.ylabel('Velocity')
        #plt.xlabel('Time')
        #plt.title("Right Hand")
        #plt.legend()

        #plt.figure(2)
        #plt.plot( times[1:(nLines)], speed[:, 21+i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        #plt.ylabel('Velocity')
        #plt.xlabel('Time')
        #plt.title("Left Hand")
        #plt.legend()

    #Calculate the acceleration
    #iiii = np.size(speed, axis=0)
    #for i in range(np.size(speed, axis=0)-2):
        #for j in range(42):
            #if check < 42 :
                #acc[i][j] =  (speed[i+1][j]-speed[i][j])/(times[i+2]+times[i+1])
                #check += 1
            #else:
                #aux[0][j] = (speed[i+1][j]-speed[i][j])/(times[i+2]+times[i+1])
        #acc = np.vstack([acc,aux])


    #for i in range(21):
        #plt.figure(3)
        #plt.plot( acc[:, i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        #plt.ylabel('Acceleration')
        #plt.title("Right Hand")
        #plt.legend()

        #plt.figure(4)
        #plt.plot( acc[:, 21+i],marker='o',linestyle='dashed', linewidth=2, markersize=10, label='joint{}'.format(i))
        #plt.ylabel('Acceleration')
        #plt.title("Left Hand")
        #plt.legend()

    #Jitter
    #jitter = np.zeros( np.size(acc, axis=0)-1)
    #for i in range(np.size(acc, axis=0)-1):
        #for j in range(42):
            ##s = acc[i+1][j] - acc[i][j]
            ##jitter[j][i] = 100 * s
            #if (acc[i][j]*acc[i+1][j] < 0):
                #jitter[i] += 1
            #else:
                #jitter[i] += -1
                ##if(jitter[i] > 0):
                  # #jitter[i] += -1
                ##else:
                    ##jitter[i] = 0
    ##for i in range(42):
    #plt.figure(5)
    #plt.plot( jitter,marker='o',linestyle='dashed', linewidth=1, markersize=10, label='Jitter')
    #plt.ylabel('Jitter')

    #plt.show()
                #f.close()














    #while True:
    #    line = f.readline()
    #    if line == '':
    #        print("Number of line in the file: ",nLines)
    #        nLines = 0
    #        break
    #    nLines += 1
