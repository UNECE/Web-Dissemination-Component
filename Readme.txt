For more details go to the page at http://www1.unece.org/stat/platform/display/pcaxis/Dissemination+Software

Quick Stats web application is an application wrapping PX-Web web application. It also provides access to the quick statistics in a way similar to the Quick Stats Mobile App. 
The user interface of the Quick Stats web application has been inspired by the mobile app. The application accepts data in two formats, Quick Stats Data Model and Quick Stats File Format. 
See the part below explaining how to confugure the web application.
The web application is located at /PXWeb/. It also contains a few other folders with asp files and quick statistics data files (see Quick Stats Mobile App).

IMPORTANT FOR UNECE ONLY: When updating the ASP .NET files for the Quick Stats web application, do not overwrite those folders at /PXWeb/Dialog and /PXWeb/QuickStatistics!
 
There is also LastUpdateDateWebSite.asp at the root fodler /PXWeb to let users check when the PX-Web files have been updated for the last time.
The Quick Stats web application is designed using the ASP .NET MVC model that suits the best to separate the data from the presentation layers. 
The mapping software is based on Leaflet.js JavaScript component. FusionCharts has been used for drawing charts.
The routing classes are changed to separate the web pages in different languages for better indexing by the search engines.
The presentation classes are designed to insure the ideas of the responsie design to make the quick statistics web application readable on the mobile devices. 
Thus the Mobile First approach has been an important part of the design and development process.
Ajax has been extensively used to make navigation faster and more convenient. Thus the controller classes send JSON files back to the presentation classes. 
Ajax approach has been used for updating maps, charts and country profiles. 
Another thing that inspired the design of the web application is the Material Design concept developed by Google. 
Although design style is not exactly the same, the headers for PX-Web subject matters, simplicity and mobile-devices friendliness come from this concept.
 
How to configure the application

The configuration is made in the Web.config file located in the root folder. The following are the application keys/parameters.
 
1. ODBC string.See example below,
<add name="QuickStatsConnection" connectionString="server=myserver.unog.un.org;database=mydatabase;uid=myuser;password=mypassword;" />
 
2. Version. See example below,
<add key="WebApplicationVersion" value="7.4" /> 
 
3. Data Reader Type defines whether Quick Stats Data Model or Quick Stats File Format will be used. Possible values are sql.transact and flatfiles.json. See example below,
<add key="DataReaderType" value="sql.transact" />
 
4. Map file path with data for Leafletjs component. See example below,
<add key="MapFileName" value="/PXWeb/Leaflet/unecemap_v2.js" />
 
5. Folder for the file with territories labels. See example below,
<add key="TerritoriesFileFolder" value="/PXWeb/Leaflet" />
 
6. Show the home page or hide. Possible values are true and false. See example below,
<add key="Show.HopePage.UNECE" value="true" />
 
7. Path to the folder with data in Quick Stats File Format. Not valid if Quick Stats Data Model is used. See example below,
<add key="FlatFiles.DataFolder" value="/PXWeb/SdmxRi/Download" />
 
8. Logging. It defines where the log is going to be saved. Possible values are sql.transact and flatfile.csv. See example below,
<add key="WebStatsLog" value="sql.transact" />
 
9. The path to the log file if flatfile.csv is chosen as the log format.
<add key="WebStatsLogFilePath" value="c:/temp/log.txt" />
 