using System.Windows;

namespace Warehouse.Themes.Resources
{
    public class Keys
    {
        #region BackgroundBrush

        private static ResourceKey _BackgroundBrush;

        public static ResourceKey BackgroundBrush
        {
            get
            {
                return _BackgroundBrush ?? (_BackgroundBrush = new ComponentResourceKey(typeof(Keys), "BackgroundBrush"));
            }
        }

        #endregion

        #region GreenLevelBrush

        private static ResourceKey _GreenLevelBrush;

        public static ResourceKey GreenLevelBrush
        {
            get
            {
                return _GreenLevelBrush ?? (_GreenLevelBrush = new ComponentResourceKey(typeof(Keys), "GreenLevelBrush"));
            }
        }

        #endregion

        #region YellowLevelBrush

        private static ResourceKey _YellowLevelBrush;

        public static ResourceKey YellowLevelBrush
        {
            get
            {
                return _YellowLevelBrush ?? (_YellowLevelBrush = new ComponentResourceKey(typeof(Keys), "YellowLevelBrush"));
            }
        }

        #endregion

        #region RedLevelBrush

        private static ResourceKey _RedLevelBrush;

        public static ResourceKey RedLevelBrush
        {
            get
            {
                return _RedLevelBrush ?? (_RedLevelBrush = new ComponentResourceKey(typeof(Keys), "RedLevelBrush"));
            }
        }

        #endregion

        #region StandardForeground

        private static ResourceKey _StandardForeground;

        public static ResourceKey StandardForeground
        {
            get
            {
                return _StandardForeground ?? (_StandardForeground = new ComponentResourceKey(typeof(Keys), "StandardForeground"));
            }
        }

        #endregion
    }
}
