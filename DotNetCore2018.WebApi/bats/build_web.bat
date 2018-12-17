@echo off
cd .\src\ui
call build.bat
cd ..\..\
if [%1]==[] goto dev
echo Building ui (%1) ...
yarn webpack --config webpack.%1.js
goto end
:dev
echo Building ui (dev) ...
yarn webpack --config webpack.dev.js
:end
echo Done!
echo on
