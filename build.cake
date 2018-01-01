var target = Argument("target", "Default");
var deploy_dir = Argument("dir", "");

Task("Default")
  .Does(() => 
  {
    CleanDirectories("build");
    MSBuild("TheLongDark.sln", configurator =>
      configurator.WithProperty("OutDir", "../../build"));
  });

Task("Deploy")
  .Does(() => 
  {
    if(deploy_dir == "")
      throw new Exception("No deploy directory specified!");

    CopyFile("build/TheLongDark.API.dll", deploy_dir + "/TheLongDark.API.dll");
    CopyFile("build/TheLongDark.ModLoader.dll", deploy_dir + "/TheLongDark.ModLoader.dll");
  });

RunTarget(target);