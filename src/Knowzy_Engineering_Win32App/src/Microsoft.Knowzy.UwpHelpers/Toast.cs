using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Microsoft.Knowzy.UwpHelpers
{
    public class Toast
    {
        static ToastNotification toast = null;
        static ToastNotifier notifier = null;

        public static void CreateToast(String xml)
        {
            if (!ExecutionMode.IsRunningAsUwp())
            {
                return;
            }

            try
            {
                if (notifier == null)
                {
                    notifier = ToastNotificationManager.CreateToastNotifier();
                }
                else
                {
                    notifier.Hide(toast);
                }
                XmlDocument toastXml = new XmlDocument();
                toastXml.LoadXml(xml);

                toast = new ToastNotification(toastXml);
                notifier.Show(toast);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateToast Error:" + ex.Message);
            }
        }
    }
}