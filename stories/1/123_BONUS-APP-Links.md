 # Add App linking to PWA (1.2.3)

This task has a dependency on [Task 1.2.1](121_Add_WIndows_Feature.md) and all of it's prerequisites.

## Task 

In this BONUS task, you want to configure your PWA to handle all requests that are send to your domain URL.  For example you should be able to create a link in a file or email that navigates to `https://YOURSITENAME.azruewebsites.net/receiving` and have that open the receiving section of your PWA, instead of your website.   This is a stretch goal. It takes a bit of manual configuration to work.

1. Update your local manifest.json (what you downloaded from pwabuilder.com).

2. Add JSON object to web site.

3. Add activation object to website.

4. Redeploy site.

5. Reinstall your local app.


## References

[https://docs.microsoft.com/en-us/windows/uwp/launch-resume/web-to-app-linking](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/web-to-app-linking) ** Note code samples in c#, but manifest changes are the same **

[https://blogs.windows.com/buildingapps/2016/03/07/hosted-web-apps-go-beyond-the-app/#JoWUjrHvp5wDFPt1.97](https://blogs.windows.com/buildingapps/2016/03/07/hosted-web-apps-go-beyond-the-app/#JoWUjrHvp5wDFPt1.97) ** Note this shows you how to do a javascript activation object to navigate your app to the URL that launched it **


## Continue to [next task >> ](124_BONUS_InMemoryCaching.md)
