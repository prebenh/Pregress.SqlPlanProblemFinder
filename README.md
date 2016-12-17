# SQL Plan problem finder

[![Build status](https://ci.appveyor.com/api/projects/status/lbc8na4m5rajug76?svg=true)](https://ci.appveyor.com/project/prebenh/pregress-sqlplanproblemfinder)

## How to use

1. Use SQL mananagment studio to save the index plan as a '.sqlplan' file.
2. Open Commandline or Powershell and run `.\Pregress.SqlPlanProblemFinder.exe 'C:\PathToYour.sqlplan'`
3. This will create a file `MissingIndexes.sql` on the location where the Commandline or Powershell is running.



## Use cases

If you want do delete records from a SQL Server database which has a lot of foreign keys, you can use this tool to generate the missing indexes.


## Output

The output file contains missing indexes for table scans and index scans. 
Creating the indexes will result in index seeks.
