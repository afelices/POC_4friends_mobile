﻿namespace POC_4friends_mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("Credentials", typeof(Credentials));
        }
    }
}
