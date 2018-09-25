@echo off
cd .\src\ui
call build.bat
cd ..\..\
echo Building ui ...
yarn webpack
echo Done!
echo on
