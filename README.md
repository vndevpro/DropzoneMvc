# DropzoneMvc
MVC controller and file upload handling in ASP.NET MVC 

# Steps to install it on your MVC App

1. Put into web.config the below keys in AppSettings
    
    <!--A place to hold all temporary files-->
    <add key="TempFilesRoot" value="~/App_Data/TempFiles" />
    
2. Install the package into your app

    Install-Package DropzoneMvc
  
3. In your view (the form) add the following

    @{
        Html.RenderAction("GetComponent", "DropzoneAttachment", new { referenceId = Model.Id });
    }
    
4. In POST action of the controller

    var tempFilesFolder = Server.MapPath(ConfigurationManager.AppSettings["TempFilesRoot"]);
    var files = new DropzoneMonitor(Request).GetActiveUploadedFiles(tempFilesFolder);

Now you get all the files, just save them as your logic requires.
