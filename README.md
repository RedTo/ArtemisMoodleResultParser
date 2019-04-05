# ArtemisMoodleResultParser
This tool is used to parse results from artemis and moodle and create a csv, which could then be inserted into the moodle gradle system.

## Artemis Export
The Artemis grading could be easily exported with the default options.

## Moodle Export
To get the best result and a nice looking import file it is recommended to use the following export settings:
 - Choose Export
 - Choose Textfile
 - Just select the exercise, which you have downloaded from Artemis
 - Feedback is at the moment not supported (uncheck)
 - Ignore blocked users (checked)
 - Check points as grading
 - Two decimal numbers are the default in the programm
 - Use semikolon as separator

## Use the programm
To use the programm just select both files in the explorer and drag & drop them onto the exe.
If the programm do not what it should do, you maybe have to install the latest .NET framework from the Microsoft website.

## Moodle Import
If you have used the programm to create the import file you can do the following to upload it on moodle:
 - Choose Import
 - Select CSV-File
 - Drag and drop or select the file
 - Coding should be UTF-8
 - Seperator is comma
 - All the other otions are irrelevant (you have to decide)

After uploading the file to moodle, the results are displayed in a nice order. Under all those entries you will find some options to specify the mapping.
 - Map ID-Number to ID-Number
 - In the column where the score is specified, select the corresponding exercise