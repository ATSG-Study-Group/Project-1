:: SET is used to set a variable to a value
SET Script=C:\Users\dave\Development\WorkProjects\ATSG StudyGroup\Project-1\Scripts\SendLPZ.ps1

:: %CD% is the environment variable for the calling directory (current directory when the script was run)
:: %n% are command line parameters when the bat file was called
SET CallingFolder=%CD%
SET Slot=%1%
SET FileName=%2%

:: Powershell command is used to execute a powershell script from a terminal command line
POWERSHELL -NoProfile -ExecutionPolicy Bypass -File "%Script%" "%FileName%" -programSlot %Slot% -LPZFolder "%CallingFolder%" -NewestInFolder -FTPMethod

:: timeout will hold the terminal session open for the specified number of seconds or until a key is pressed
timeout /t 30