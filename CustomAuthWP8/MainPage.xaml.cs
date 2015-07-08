/*
 * COPYRIGHT LICENSE: This information contains sample code provided in source code form. You may copy, modify, and distribute
 * these sample programs in any form without payment to IBM® for the purposes of developing, using, marketing or distributing
 * application programs conforming to the application programming interface for the operating platform for which the sample code is written.
 * Notwithstanding anything to the contrary, IBM PROVIDES THE SAMPLE SOURCE CODE ON AN "AS IS" BASIS AND IBM DISCLAIMS ALL WARRANTIES,
 * EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, ANY IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, SATISFACTORY QUALITY,
 * FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND ANY WARRANTY OR CONDITION OF NON-INFRINGEMENT. IBM SHALL NOT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR OPERATION OF THE SAMPLE SOURCE CODE.
 * IBM HAS NO OBLIGATION TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS OR MODIFICATIONS TO THE SAMPLE SOURCE CODE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CustomAuthWP8.Resources;
using Newtonsoft.Json.Linq;
using IBM.Worklight;

namespace CustomAuthWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WLClient client;
        public WindowsChallengeHandler challengeHandler;
        public static MainPage _this;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _this = this;

            AboutBox.Text = "IBM MobileFirst Platform\nCustom Login Module Authentication Sample";
            ReceivedTextBlock.Text = "Connecting...\n";
            client = WLClient.getInstance();
            challengeHandler = new WindowsChallengeHandler();
            client.registerChallengeHandler((BaseChallengeHandler<JObject>)challengeHandler);
            client.connect(new MyConnectResponseListener(this));
        }

        private void InvokeProcedure_Click(object sender, RoutedEventArgs e)
        {
            WLProcedureInvocationData invokeData = new WLProcedureInvocationData("AuthAdapter", "getSecretData");
            WLRequestOptions options = new WLRequestOptions();
            client.invokeProcedure(invokeData, new MyResponseListener(this), options);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please start the application again.");
            Application.Current.Terminate();
        }

        public void AddTextToReceivedTextBlock(String param)
        {
            ReceivedTextBlock.Text = param;
        }
    }
}