/**
* Copyright 2015 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
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