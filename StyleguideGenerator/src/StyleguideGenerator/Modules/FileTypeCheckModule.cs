using StyleguideGenerator.Models.Data;

namespace StyleguideGenerator.Modules
{
    public static class FileTypeCheckModule
    {
        public static ProjectFileType Check(string filename)
        {
            if (filename.EndsWith(".css"))
            {
                return ProjectFileType.Css;
            }
            else if(filename.EndsWith(".js"))
            {
                return ProjectFileType.Js;
            }
            return ProjectFileType.None;
        }
    }
}
