 # Adapted from: https://github.com/Ankit0822/Hand-Tracking-using-opencv-python-and-mediapipe
# Prereqs:
# pip install opencv-python
# pip install mediapipe

import cv2
import mediapipe as mp
import time
import socket
import numpy as np
from struct import *

from google.protobuf.json_format import MessageToDict


def calculate_angle_right(image, results, joint_list):
    closedFingersRight = 0;
    #Loop through joint sets
    for joint in joint_list:
        a1 = np.array([results.right_hand_landmarks.landmark[joint[0]].x, results.right_hand_landmarks.landmark[joint[0]].y]) # First coord
        b1 = np.array([results.right_hand_landmarks.landmark[joint[1]].x, results.right_hand_landmarks.landmark[joint[1]].y]) # Second coord
        c1 = np.array([results.right_hand_landmarks.landmark[joint[2]].x, results.right_hand_landmarks.landmark[joint[2]].y]) # Third coord

        radians1 = np.arctan2(c1[1] - b1[1], c1[0]-b1[0]) - np.arctan2(a1[1]-b1[1], a1[0]-b1[0])
        angle1 = np.abs(radians1*180.0/np.pi)

        if angle1 > 180.0:
            angle1 = 360-angle1

        #Verify if the angle is below 100 degrees
        if angle1 < 110:
            closedFingersRight+=1;

        cv2.putText(image, str(round(angle1, 2)), tuple(np.multiply(b1, [1920, 1080]).astype(int)),
                cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2, cv2.LINE_AA)

    if closedFingersRight >= 4:
        print("Right Hand Close")
        return 1

    return 0

def calculate_angle_left(image, results, joint_list):
    closedFingersLeft = 0;
    #Loop through joint sets
    for joint in joint_list:
        a2 = np.array([results.left_hand_landmarks.landmark[joint[0]].x, results.left_hand_landmarks.landmark[joint[0]].y]) # First coord
        b2 = np.array([results.left_hand_landmarks.landmark[joint[1]].x, results.left_hand_landmarks.landmark[joint[1]].y]) # Second coord
        c2 = np.array([results.left_hand_landmarks.landmark[joint[2]].x, results.left_hand_landmarks.landmark[joint[2]].y]) # Third coord

        radians2 = np.arctan2(c2[1] - b2[1], c2[0]-b2[0]) - np.arctan2(a2[1]-b2[1], a2[0]-b2[0])
        angle2 = np.abs(radians2*180.0/np.pi)

        if angle2 > 180.0:
            angle2 = 360-angle2

        #Verify if the angle is below 100 degrees
        if angle2 < 110:
            closedFingersLeft+=1;

        cv2.putText(image, str(round(angle2, 2)), tuple(np.multiply(b2, [1920, 1080]).astype(int)),
                cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2, cv2.LINE_AA)

    if closedFingersLeft >= 4:
        print("Left Hand Close")
        return 1

    return 0


UDP_IP = "127.0.0.1"
UDP_PORT = 5005

print("UDP target IP: %s" % UDP_IP)
print("UDP target port: %s" % UDP_PORT)

sock = socket.socket(socket.AF_INET, # Internet
                     socket.SOCK_DGRAM) # UDP

# Create Video object
cap = cv2.VideoCapture(1)
width = 1920
height = 1080
cap.set(cv2.CAP_PROP_FRAME_WIDTH, width)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, height)
# Formality we have to write before start
# using this model
#mpHands = mp.solutions.hands
# Creating an object from class Hands
mp_holistic = mp.solutions.holistic
#hands = mpHands.Hands()
# creating an object to draw hand landmarks
mpDraw = mp.solutions.drawing_utils
# Previous time for frame rate
pTime = 0
# Current time for frame rate
cTime = 0

joint_list = [[7,6,5], [11,10,9], [15,14,13], [19,18,17], [4,3,2]]

with mp_holistic.Holistic(min_detection_confidence=0.5, min_tracking_confidence=0.5) as holistic:
    while True:
        # Getting our Frame
        success, img = cap.read()
        # Convert image into RGB
        img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
        # Calling the hands object to the getting results
        results = holistic.process(img)
        #img = cv2.flip(img, 1)
        #print(results.multi_hand_landmarks)
        img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)

        # Checking if the right hand was detected
        if results.right_hand_landmarks :
            check1 = calculate_angle_right(img, results, joint_list)
            for i in range(21):
                coord_struct = pack('!ffffff', float(i), results.right_hand_landmarks.landmark[i].x, results.right_hand_landmarks.landmark[i].y, results.right_hand_landmarks.landmark[i].z, float(0), float(check1))

                sock.sendto(coord_struct, (UDP_IP, UDP_PORT))
                # Height, width and channel of image
                h, w, c = img.shape
                # X and Y coordinate
                # their values in decimal so
                # we have to convert into pixel
                cx, cy = int(results.right_hand_landmarks.landmark[i].x*w),int(results.right_hand_landmarks.landmark[i].y*h)
                #print(id, cx, cy)
                #if id ==4:
                cv2.circle(img, (cx, cy), 7, (255,0,255), cv2.FILLED)
            # 2. Right hand
            mpDraw.draw_landmarks(img, results.right_hand_landmarks, mp_holistic.HAND_CONNECTIONS,
                                     mpDraw.DrawingSpec(color=(121,22,76), thickness=2, circle_radius=4),
                                     mpDraw.DrawingSpec(color=(121,44,250), thickness=2, circle_radius=2)
                                     )



        # Checking if the left hand was detected
        if results.left_hand_landmarks :
            check2 = calculate_angle_left(img, results, joint_list)
            for i in range(21):
                coord_struct = pack('!ffffff', float(i), results.left_hand_landmarks.landmark[i].x, results.left_hand_landmarks.landmark[i].y, results.left_hand_landmarks.landmark[i].z, float(1), float(check2))

                sock.sendto(coord_struct, (UDP_IP, UDP_PORT))
                # Height, width and channel of image
                h, w, c = img.shape
                # X and Y coordinate
                # their values in decimal so
                # we have to convert into pixel
                cx, cy = int(results.left_hand_landmarks.landmark[i].x*w),int(results.left_hand_landmarks.landmark[i].y*h)
                #print(id, cx, cy)
                #if id ==4:
                cv2.circle(img, (cx, cy), 7, (255,0,255), cv2.FILLED)
            # 2. Left hand
            mpDraw.draw_landmarks(img, results.left_hand_landmarks, mp_holistic.HAND_CONNECTIONS,
                                     mpDraw.DrawingSpec(color=(121,22,76), thickness=2, circle_radius=4),
                                     mpDraw.DrawingSpec(color=(121,44,250), thickness=2, circle_radius=2)
                                     )


        # Getting the current time
        cTime = time.time()
        # Getting frame per second
        fps = 1/(cTime-pTime)
        # Previous time become current time
        pTime = cTime
        # Labeling the Frame rate
        cv2.putText(img,str(int(fps)),(10,70),cv2.FONT_HERSHEY_PLAIN,3,
                                (255,0,255),3)

        cv2.imshow('Raw Webcam Feed', img)
        if cv2.waitKey(10) & 0xFF == ord('q'):
            break



#while True:
    # Getting our Frame
    #success, img = cap.read()
    # Convert image into RGB
    #imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    # Calling the hands object to the getting results
    #results = hands.process(imgRGB)
    #print(results.multi_hand_landmarks)

    # Checking something is detected or not
    #if results.multi_hand_landmarks :
        # Extracting the multiple hands
        # Go through each hand
        #for handLms in results.multi_hand_landmarks:
            ##COMMENTED THE NEXT 4 LINES OF CODE
            #if(MessageToDict(results.multi_handedness[0]).get("classification")[0].get("label") == "Right"):
                #print("Left")
            #elif(MessageToDict(results.multi_handedness[0]).get("classification")[0].get("label") == "Left"):
                #print("Right")
            # Getting id(index number) and landmark of each hand
            #for id, lm in enumerate(handLms.landmark):
                #print(id,lm)
                #print(type(id))
                #bytes object containing the values packed according to the format
                ###ADDED THESE IFS
                ##IT WAS
                #coord_struct = pack('!ffff', float(id), lm.x, lm.y, lm.z)
                #if(MessageToDict(results.multi_handedness[0]).get("classification")[0].get("label") == "Right"):
                #    coord_struct = pack('!fffff', float(id), lm.x, lm.y, lm.z, 0)
                #    #print("Left")
                #elif(MessageToDict(results.multi_handedness[0]).get("classification")[0].get("label") == "Left"):
                #    coord_struct = pack('!fffff', float(id), lm.x, lm.y, lm.z, 1)
                    #print("Right")
                #print(coord_struct)
                #send via UDP
                #sock.sendto(coord_struct, (UDP_IP, UDP_PORT))
                # Height, width and channel of image
                #h, w, c = img.shape
                # X and Y coordinate
                # their values in decimal so
                # we have to convert into pixel
                #cx, cy = int(lm.x*w),int(lm.y*h)
                #print(id, cx, cy)
                #if id ==4:
                #cv2.circle(img, (cx, cy), 7, (255,0,255), cv2.FILLED)
            # Draw the landmarks and line of the each hands
            #mpDraw.draw_landmarks(img, handLms, mpHands.HAND_CONNECTIONS)
    # Getting the current time
    #cTime = time.time()
    # Getting frame per second
    #fps = 1/(cTime-pTime)
    # Previous time become current time
    #pTime = cTime
    # Labeling the Frame rate
    #cv2.putText(img,str(int(fps)),(10,70),cv2.FONT_HERSHEY_PLAIN,3,
                            #(255,0,255),3)

    #cv2.imshow("image",img) # Show the frame
    #cv2.waitKey(1) # Wait for 1 millisecond
