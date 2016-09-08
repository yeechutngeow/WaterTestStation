using System;
using System.Windows.Forms;

namespace WaterTestStation
{
	public class FormUtil
	{
		private delegate void SetLabelCallback(Label label, string text);

		public void ThreadSafeSetLabel(Label label, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (label.InvokeRequired)
			{
				SetLabelCallback d = ThreadSafeSetLabel;
				label.Invoke(d, new object[] { label, text });
			}
			else
			{
				label.Text = text;
			}
		}

		private delegate void SetStatusStripLabelCallback(StatusStrip statusStrip, ToolStripStatusLabel label, string text);

		public void ThreadSafeSetStatusStripLabel(StatusStrip statusStrip, ToolStripStatusLabel label, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (statusStrip.InvokeRequired)
			{
				SetStatusStripLabelCallback d = ThreadSafeSetStatusStripLabel;
				statusStrip.Invoke(d, new object[] {statusStrip, label, text });
			}
			else
			{
				label.Text = text;
			}
		}


		private delegate void SetTextCallback(Control control, string text);

		public void ThreadSafeSetText(Control control, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (control.InvokeRequired)
			{
				SetTextCallback d = ThreadSafeSetText;
				control.Invoke(d, new object[] { control, text });
			}
			else
			{
				control.Text = text;
			}
		}

		public string ThreadSafeReadText(Control control)
		{
			string text = null;
			if (control.InvokeRequired)
			{
				control.Invoke((MethodInvoker)delegate
				{
					text = control.Text;
				});
			}
			else
				text = control.Text;

			return text;
		}

		//private delegate Object ReadComboboxCallback(ComboBox cbo);

		public Object ThreadSafeReadCombo(ComboBox cbo)
		{
			Object value = null;
			if (cbo.InvokeRequired)
			{
				cbo.Invoke((MethodInvoker)delegate
				{
					value = cbo.SelectedValue;
				});
			}
			else
				value = cbo.SelectedValue;

			return value;
		}

		public Object ThreadSafeReadComboItem(ComboBox cbo)
		{
			Object value = null;
			if (cbo.InvokeRequired)
			{
				cbo.Invoke((MethodInvoker)delegate
				{
					value = cbo.SelectedItem;
				});
			}
			else
				value = cbo.SelectedText;

			return value;
		}

		private delegate void SetControlEnabledCallback(Control control, bool enabled);

		public void ThreadSafeSetControlEnabled(Control control, bool enabled)
		{
			if (control.InvokeRequired)
			{
				SetControlEnabledCallback d = ThreadSafeSetControlEnabled;
				control.Invoke(d, new object[] { control, enabled });
			}
			else
				control.Enabled = enabled;

		}
	}
}
