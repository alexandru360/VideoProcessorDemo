# Video Procesor Demo

This project is a 2 part project:

First make sure you have the ffmpeg.exe in api\VideoProcessingTools always copy property visual studio set to true!

Do the below

####### API - DotNet Core
````
cd into Api
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet restore
dotnet run
````

####### View - Angular 10
````
npm i -g yarn npm
cd into view
yarn
ng serve
````

####### To Do
* Maybe integrate with Angular Material
* Do an upload control for 4K Videos instead of processing the single one from sample folder
* Investigate the ffmpeg.exe capabilities.

