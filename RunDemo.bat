@ECHO OFF
set mypath=%cd%
START %mypath%\Builds\Win-x86_64\Builds.exe
TIMEOUT 0
python %mypath%\Python_Client\openCVhandtrack.py
