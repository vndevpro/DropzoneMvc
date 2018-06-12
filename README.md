# DropzoneMvc
MVC controller and file upload handling in ASP.NET MVC

/!\ JQuery must be configured & put in head tag

/!\ Dropzone js and css bundles must be declared (in BundleConfig.cs file) -> these bundles will be referenced by the view _DropzoneComponent.cshtml (in **DropzoneMvcDemo\Views\Shared**)

/!\ You must have to config an IoC for IDropzoneAttachmentSecurityCheck => Check the demo setup with SimpleInjector

# Steps to install it on your MVC App

1. Put into web.config the below keys in AppSettings (this will be used by DropzoneAttachmentController)

```
    <!--A place to hold all temporary files-->
    <add key="TempFilesRoot" value="~/App_Data/TempFiles" />
```
    
2. Install the package into your app

    Install-Package DropzoneMvc
  
3. In your view (the form) add the following

    @{
        Html.RenderAction("GetComponent", "DropzoneAttachment", new { referenceId = Model.Id });
    }
    
4. In POST action of the controller

    var tempFilesFolder = Server.MapPath(ConfigurationManager.AppSettings["TempFilesRoot"]);
    var files = new DropzoneMonitor(Request).GetActiveUploadedFiles(tempFilesFolder);
    // file will be the full path minis the TempFilesRoot

Now you get all the files, just save them as your logic requires.
