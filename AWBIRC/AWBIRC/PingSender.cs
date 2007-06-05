
using System;
using System.Threading;

/*
 *  Class that sends PING to irc server every 15 seconds
 */
class PingSender
{
    static string PING = "PING :";
    private Thread pingSender;
    // Empty constructor makes instance of Thread
    public PingSender()
    {
        pingSender = new Thread(new ThreadStart(this.Run));
    }

    // Starts the thread
    public void Start()
    {
        pingSender.Start();
    }

    // Send PING to irc server every 15 seconds
    public void Run()
    {
        while (true)
        {
            try
            {
                IrcBot.ircwriter.WriteLine(PING + IrcBot.server);
            }
            catch (ObjectDisposedException)
            {
                pingSender.Abort();
            }

            IrcBot.ircwriter.Flush();
            Thread.Sleep(15000);
        }
    }
}
