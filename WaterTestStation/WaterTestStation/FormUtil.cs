using System;
using System.Windows.Forms;

namespace WaterTestStation
{
	public class FormUtil
	{
		private delegate void SetLabelCallback(Label label, string text);

		protected void ThreadSafeSetLabel(Label label, string text)
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

		private delegate void SetTextCallback(TextBox txtBox, string text);

		protected void ThreadSafeSetText(TextBox txtBox, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (txtBox.InvokeRequired)
			{
				SetTextCallback d = ThreadSafeSetText;
				txtBox.Invoke(d, new object[] { txtBox, text });
			}
			else
			{
				txtBox.Text = text;
			}
		}

		protected string ThreadSafeReadText(Control control)
		{
			string text = null;
			if (control.InvokeRequired)
			{
				//ReadTextCallback d = ThreadSafeReadText;
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

		protected Object ThreadSafeReadCombo(ComboBox cbo)
		{
			Object value = null;
			if (cbo.InvokeRequired)
			{
				//ReadComboboxCallback d = (ReadComboboxCallback) ThreadSafeReadCombo(cbo);
				cbo.Invoke((MethodInvoker)delegate
				{
					value = cbo.SelectedValue;
				});
			}
			else
				value = cbo.SelectedValue;

			return value;
		}


	}
}
