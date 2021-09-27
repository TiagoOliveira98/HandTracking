#!/bin/sh
rootpath=$(pwd)
echo $rootpath
$rootpath/Builds/Linux-x86_64/Build.x86_64 &
python3 $rootpath/Python_Client/openCVhandtrack.py
wait
