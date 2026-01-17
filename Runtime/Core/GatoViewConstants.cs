// ------------------------------------------------------------------------
//     Copyright (c) 2026 @thatgato, @thegatoworks on GitHub
// 
//     GatoView - GatoViewConstants.cs (2026. 01. 04.)
// 
//     This file is a part of the GatoView project, which is licensed under the
//     GNU GPLv3 license. See LICENSE.md for more details.
// 
//     Purpose:
// ------------------------------------------------------------------------

namespace GatoView.Core {
    public struct Version {
        public uint Major;
        public uint Minor;
        public uint Patch;

        public Version( uint major, uint minor, uint patch )
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public bool IsOutdated( uint major, uint minor, uint patch ) => Major < major || Minor < minor || Patch < patch;
        public override string ToString() => $"{Major}.{Minor}.{Patch}";
    }


    public static class GatoViewConstants {
        public static Version AssetVersion     = new(1, 0, 0);
        public static Version InstallerVersion = new(1, 0, 0);
    }
}