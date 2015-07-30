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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using IBM.Worklight;

namespace CustomAuthWP8
{
    public class MyConnectResponseListener : WLResponseListener
    {
        CustomAuthWP8.MainPage MyMainPage;

        public MyConnectResponseListener(CustomAuthWP8.MainPage page)
        {
            MyMainPage = page;
        }

        public void onSuccess(WLResponse response)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MyMainPage.AddTextToReceivedTextBlock("Connected Successfully");
                MessageBox.Show("Connected Successfully");
            });
        }

        public void onFailure(WLFailResponse response)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MyMainPage.AddTextToReceivedTextBlock("Connection failure : " + response.getErrorMsg());
                MyMainPage.PanoramaControl.SetValue(Panorama.SelectedItemProperty, MyMainPage.Console);
                MyMainPage.Measure(new Size());
            });
        }
    }
}