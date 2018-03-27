using System;
using System.IO;

using AppKit;
using Foundation;

public class OutputWriter
{
    private const int MAX_BUF_SIZE = (1024000 * 1000); // 1 GB then flush
    private NSTextView output = new NSTextView();
    private NSMutableAttributedString buffer = new NSMutableAttributedString();
    private NSAttributedString text = new NSAttributedString("");

    public OutputWriter(NSTextView writer)
    {
        output = writer;
    }

    public void Append(String write_str)
    {
        text = new NSAttributedString(write_str);
        buffer.Append(text);
        output.TextStorage.Append(buffer);

        if (SizeOfBuffer() >= MAX_BUF_SIZE)
        {
            output.TextStorage.Append(new NSAttributedString("\nBUFFER LENGTH: " + buffer.Length.ToString() + "\n"));
            output.TextStorage.Append(new NSAttributedString("Memory cleaning: clear ouput buffer!\n"));
            output.TextStorage.Dispose();

            // free up some memory
            buffer = null;
            buffer = new NSMutableAttributedString();
        }
        text = null;
    }

    public void FlushBufferToFile()
    {
        
    }

    public int SizeOfBuffer()
    {
        return Int32.Parse(buffer.Length.ToString());
    }
}
