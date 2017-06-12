 # Add App linking to PWA (1.2.3)

This task has a dependency on [Task 1.2.1](121_Add_WIndows_Feature.md) and all of it's prerequisites.

## Task 

In this BONUS task, you want to configure your PWA to handle all requests that are send to your domain URL.  For example you should be able to create a link in a file or email that navigates to `https://YOURSITENAME.azruewebsites.net/receiving` and have that open the receiving section of your PWA, instead of your website.   This is a stretch goal. It takes a bit of manual configuration to work.

1. Update your local manifest.json (what you downloaded from pwabuilder.com).

2. Add JSON object to web site.

3. Add activation object to website.

4. Redeploy site.

5. Reinstall your local app.

**Tip:** Make sure your ASP.NET MVC site can serve up the windows-app-web-link file.  A simple way to do this for testing is to enable known file types to be served - modify your StartUp.cs / Configure method to look as follows:

        app.UseStaticFiles(new StaticFileOptions()
        {
           ServeUnknownFileTypes = true,
           DefaultContentType = "text/plain"
        });

        app.UseStaticFiles();

        app.UseMvc(routes =>
        {
        ...

**Tip:** In the generated windows10 package, you can either develop via the source\App.jsproj project in Visual Studio, or you can use your favorite editor with the manifest\appxmanifest.xml file and use the test_install.ps1 script to deploy. 

**Tip:** Remember that Apps for Websites is a Windows 10 Anniversary Update feature.

If using Visual Studio, make sure your Project Properties Target version is at least "10.0.14393".

If using the appxmanifest.xml, make sure your MaxVersionTested property has a version number of at least "10.0.14393".

## References

[https://docs.microsoft.com/en-us/windows/uwp/launch-resume/web-to-app-linking](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/web-to-app-linking) ** Note code samples in c#, but manifest changes are the same **

[https://blogs.windows.com/buildingapps/2016/03/07/hosted-web-apps-go-beyond-the-app/#JoWUjrHvp5wDFPt1.97](https://blogs.windows.com/buildingapps/2016/03/07/hosted-web-apps-go-beyond-the-app/#JoWUjrHvp5wDFPt1.97) ** Note this shows you how to do a javascript activation object to navigate your app to the URL that launched it **


## Continue to [next task >> ](124_BONUS_InMemoryCaching.md)
