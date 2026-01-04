namespace GatoView.Core
{
    public struct Version
    {
        public uint Major;
        public uint Minor;
        public uint Patch;

        public Version(uint major, uint minor, uint patch)
        {
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
        }

        public bool IsOutdated(uint major, uint minor, uint patch) => this.Major < major || this.Minor < minor || this.Patch < patch;
        public override string ToString() => $"{this.Major}.{this.Minor}.{this.Patch}";
    }
    

    public static class GatoViewConstants
    {
        public static Version AssetVersion = new(1,0,0);
        public static Version InstallerVersion = new(1,0,0);
    }
}