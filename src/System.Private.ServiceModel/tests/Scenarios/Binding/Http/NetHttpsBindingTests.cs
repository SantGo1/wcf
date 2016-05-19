﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public static class Binding_Http_NetHttpsBindingTests
{
    [Fact]
    [OuterLoop]
    [ActiveIssue(1123, PlatformID.AnyUnix)]
    public static void DefaultCtor_NetHttps_Echo_RoundTrips_String()
    {
        string testString = "Hello";
        ChannelFactory<IWcfService> factory = null;
        IWcfService serviceProxy = null;

        try
        {
            // *** SETUP *** \\
            NetHttpsBinding netHttpsBinding = new NetHttpsBinding();
            factory = new ChannelFactory<IWcfService>(netHttpsBinding, new EndpointAddress(Endpoints.HttpBaseAddress_NetHttps));
            serviceProxy = factory.CreateChannel();

            // *** EXECUTE *** \\
            string result = serviceProxy.Echo(testString);

            // *** VALIDATE *** \\
            Assert.True(String.Equals(testString, result), String.Format("Expected result was {0}. Actual was {1}", testString, result));

            // *** CLEANUP *** \\
            factory.Close();
            ((ICommunicationObject)serviceProxy).Close();
        }
        finally
        {
            // *** ENSURE CLEANUP *** \\
            ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
        }
    }

    [Fact]
    [OuterLoop]
    public static void TransportWithMessageCredential_NotSupported_NetHttps()
    {
        string testString = "Hello";
        ChannelFactory<IWcfService> factory = null;
        IWcfService serviceProxy = null;

        // BasicHttpsSecurityMode.TransportWithMessageCredential is accessible but not supported.
        // Verify the correct exception and message is thrown.
        // When/if Message Security is supported this test will fail and will serve as a reminder to add test coverage.
        Assert.Throws<PlatformNotSupportedException>(() =>
        {
            try
            {
                // *** SETUP *** \\
                NetHttpsBinding netHttpsBinding = new NetHttpsBinding(BasicHttpsSecurityMode.TransportWithMessageCredential);
                factory = new ChannelFactory<IWcfService>(netHttpsBinding, new EndpointAddress(Endpoints.HttpBaseAddress_NetHttps));
                serviceProxy = factory.CreateChannel();

                // *** EXECUTE *** \\
                string result = serviceProxy.Echo(testString);
            }
            finally
            {
                // *** ENSURE CLEANUP *** \\
                ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
            }
        });
    }

    [Fact]
    [OuterLoop]
    [ActiveIssue(1123, PlatformID.AnyUnix)]
    public static void NonDefaultCtor_NetHttps_Echo_RoundTrips_String()
    {
        string testString = "Hello";
        ChannelFactory<IWcfService> factory = null;
        IWcfService serviceProxy = null;

        try
        {
            // *** SETUP *** \\
            NetHttpsBinding netHttpsBinding = new NetHttpsBinding(BasicHttpsSecurityMode.Transport);
            factory = new ChannelFactory<IWcfService>(netHttpsBinding, new EndpointAddress(Endpoints.HttpBaseAddress_NetHttps));
            serviceProxy = factory.CreateChannel();

            // *** EXECUTE *** \\
            string result = serviceProxy.Echo(testString);

            // *** VALIDATE *** \\
            Assert.NotNull(result);
            Assert.Equal(testString, result);

            // *** CLEANUP *** \\
            factory.Close();
            ((ICommunicationObject)serviceProxy).Close();
        }
        finally
        {
            // *** ENSURE CLEANUP *** \\
            ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
        }
    }
}