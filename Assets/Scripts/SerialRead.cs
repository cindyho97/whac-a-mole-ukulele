/* Testprog serial port from Arduino threaded
 * Wim Van Weyenberg
 * 22/08/2017
 * In Start() wordt de comport geopend (stel juiste naam in van de Arduino Port!) en wordt ook de thread gestart om data te ontvangen.
 * Als er data ontvangen is wordt deze in update getoond via Debug.Log
 */


using UnityEngine;
using System.Threading;
using System.IO.Ports;
using System;

public class SerialRead : MonoBehaviour
{
    private SerialPort sp; // = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
    private bool comportFound = false; //if comportFound is true the selected comport is open
    static private char databyte_in; //gelezen databyte van seriële poort
    static private bool databyteRead = false; //wordt true als er een databyte ontvangen is in seriële thread
    private short previousNoteValue;
    private short currentNoteValue;

    //threadrelated
    private bool stopSerialThread = false; //om de thread te kunnen stoppen
    private Thread readSerialThread; //threadvariabele
    //static private string strOntvangen = "";

    void Start()
    {
        OpenConnection(); //init COMPort
                          //thread aanmaken en starten
        if (comportFound)
        {
            readSerialThread = new Thread(SerialThread); //readThread is een instance die de functie threadfunc gebruikt
            readSerialThread.Start(); //thread starten
        }
    }

    void Update()
    {
        if (databyteRead) //als een databyte ontvangen is
        {
            databyteRead = false; //om volgende databyte te kunnen ontvangen

            currentNoteValue = (short)databyte_in;
            if(currentNoteValue > 0 && currentNoteValue != previousNoteValue)
            {
                Debug.Log(currentNoteValue.ToString());
            }
            
            previousNoteValue = currentNoteValue;

            //Debug.Log(strOntvangen);
            //if (databyte_in != '\t')
            //{
            //    strOntvangen = databyte_in.ToString();
            //}
            //else
            //{
            //    Debug.Log("Gelezen: " + strOntvangen.Length);
            //    strOntvangen = "";
            //}
        }
    }


    void SerialThread() //de aparte thread is best nodig omdat we de timeout lang moeten wachten voor we weten of er een karakter te ontvangen is
    {
        while (!stopSerialThread) //mooi afsluiten van de thread bij verlaten programma
        {
            if (comportFound)
            {

                try //probeer steeds iets te ontvangen
                {
                    databyte_in = (char)sp.ReadByte();
                    //strOntvangen = sp.ReadLine();
                    //Debug.Log("ReadByte: " + databyte_in);
                    databyteRead = true;
                }
                catch (Exception)
                {
                    //Debug.Log(e.Message);
                }
            }
        }
    }


    /* Function connecting to serial PORT
     * Start with COM1 to COM99
     * Try to open the COMport: if succes try to send 'A' en receive 'U' to detect the wanted comport
     */
    public void OpenConnection()
    {
        sp = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        sp.Open();  // opens the connection
        comportFound = true;
        //string comportName = "";
        //short comportNumber = 1;
        //bool blnPortcanopen = false;


        //do
        //{
        //    blnPortcanopen = true; //suppose is will work to open the comport
        //    try //try to open a selected comport
        //    {
        //        comportName = "COM" + comportNumber.ToString();
        //        Debug.Log("Trying to open comport: " + comportName);
        //        sp = new SerialPort(comportName, 9600, Parity.None, 8, StopBits.One);
        //        sp.Open();  // opens the connection
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e.Message);
        //        blnPortcanopen = false;
        //        sp = null;
        //        comportNumber++;
        //    }
        //    if (blnPortcanopen) //is it the right comport we search?
        //    {
        //        bool comPortFound = false;
        //        sp.ReadTimeout = 20;  // sets the timeout value before reporting error
        //        Debug.Log("Port Opened!" + comportName);
        //        Debug.Log("Testing response to A");
        //        char[] charArray = { 'A' };
        //        sp.Write(charArray, 0, 1);
        //        try //probeer 'U' te ontvangen van Arduino als respons
        //        {
        //            if ((char)sp.ReadByte() == 'U')
        //            {
        //                Debug.Log("COMPORT for Unity found on " + comportName);
        //                comPortFound = true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Debug.Log(e.Message);
        //        }
        //        if (comPortFound == false)
        //        {
        //            Debug.Log("Opened wrong comport " + comportName);
        //            sp.Close();
        //            sp = null;
        //        }
        //    }
        //}
        //while ((!comportFound) && comportNumber <= 99);
        //if(!comportFound)
        //{
        //    Debug.Log("No comport found for Unity");
        //}
    }


    void OnApplicationQuit() //proper afsluiten van de thread
    {
        if (sp != null) sp.Close();
        stopSerialThread = true;
        if (readSerialThread != null) readSerialThread.Abort();
    }
}
