﻿#pragma checksum "..\..\WindowEditServiceClient.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "216AC82B88E66224FF42E67133BCBB2D18E0D830"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;
using гостиницы_ласт;


namespace гостиницы_ласт {
    
    
    /// <summary>
    /// WindowEditServiceClient
    /// </summary>
    public partial class WindowEditServiceClient : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\WindowEditServiceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RegistrationIDCB;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\WindowEditServiceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ServiceIDCB;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\WindowEditServiceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusCB;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\WindowEditServiceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBN;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\WindowEditServiceClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBN;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/гостиницы ласт;component/windoweditserviceclient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowEditServiceClient.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RegistrationIDCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\WindowEditServiceClient.xaml"
            this.RegistrationIDCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RegistrationIDCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ServiceIDCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 15 "..\..\WindowEditServiceClient.xaml"
            this.ServiceIDCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ServiceIDCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.StatusCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\WindowEditServiceClient.xaml"
            this.StatusCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.StatusCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SaveBN = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\WindowEditServiceClient.xaml"
            this.SaveBN.Click += new System.Windows.RoutedEventHandler(this.SaveBN_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CancelBN = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\WindowEditServiceClient.xaml"
            this.CancelBN.Click += new System.Windows.RoutedEventHandler(this.CancelBN_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

