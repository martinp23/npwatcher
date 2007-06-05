
using System;
using System.Threading;

/*
 *  Class that sends PING to irc server every 15 seconds
 *  Except it doesn't.  It actually does the automatic SVN-checking thingummy.
 */
class CheckSVN
{
    
    private Thread checkSVN;
    // Empty constructor makes instance of Thread
    public CheckSVN()
    {
        checkSVN = new Thread(new ThreadStart(this.Run));
    }

    // Starts the thread
    public void Start()
    {
        checkSVN.Start();
    }

    // Send PING to irc server every 15 seconds
    public void Run()
    {
        while (true)
        {
            try
            {
                IrcBot.talkNormal("checkSVN1");
            }
            catch (ObjectDisposedException)
            {
                checkSVN.Abort();
            }

            IrcBot.ircwriter.Flush();
            Thread.Sleep(90000);
        }
    }
}
