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
    static public bool databyteRead = false; //wordt true als er een databyte ontvangen is in seriële thread
    private short previousNoteValue;
    public short currentNoteValue;
    public bool noteDetected;

    //threadrelated
    private bool stopSerialThread = false; //om de thread te kunnen stoppen
    private Thread readSerialThread; //threadvariabele

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            noteDetected = true;
            currentNoteValue = 45;
        }
        //Debug.Log("databyte_in: " + databyte_in);
        CheckNotePlayed();  
    }

    public bool CheckNotePlayed()
    {
        if (databyteRead) //als een databyte ontvangen is
        {
            databyteRead = false; //om volgende databyte te kunnen ontvangen
            currentNoteValue = 0;
            currentNoteValue = (short)databyte_in;

            if (currentNoteValue > 0 && currentNoteValue != previousNoteValue)
            {
                Debug.Log(currentNoteValue.ToString());
                previousNoteValue = currentNoteValue;
                noteDetected = true;
            }
            else
            {
                currentNoteValue = 0;
                noteDetected = false;
            }
        }
        return noteDetected;   
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
    }


    void OnApplicationQuit() //proper afsluiten van de thread
    {
        if (sp != null) sp.Close();
        stopSerialThread = true;
        if (readSerialThread != null) readSerialThread.Abort();
    }
}
