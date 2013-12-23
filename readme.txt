Original plan:

The script is suppose to check the destination directory and 
look for a folder with the current year and month. If no such 
folder is found it is suppose to create one. Then it is supposed 
to check the source directory for any files older than 7 days. 
for each file that is over seven days, it is suppose to check the 
creation time for year and month and put the file in the matching 
folder in the destination directory.

Execution:

Instead of checking to be sure that a path exists for the current
year/month, it checks for each file. If it finds one from July, 
1978, it will put it in a folder named "197807".

In theory, what you would do with this is set it up to run as a 
scheduled task after configuring all the paths you want it to archive.
Each one would be added as a <path /> element.

<archiverConfig isConfigured="true">
	<path source="testing" archive="testing/also" />
	<path source="nonArchive" archive="archive" />
</archiverConfig>