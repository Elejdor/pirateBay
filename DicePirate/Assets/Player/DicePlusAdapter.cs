using UnityEngine;
using System.Collections;

public class DicePlusAdapter : MonoBehaviour, IDicePlusListener, IDicePlusConnectorListener
{
    DicePlus myDice;
    bool refreshed = false;
    bool firstTime = true;
    public bool DiceConnected { get; private set; }

    public Vector3 startDirection = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    private bool firstReadOut = true;
    public float Lean { get; private set; }
    public static DicePlusAdapter instance = null;

    void Awake()
    {
        Lean = 0.0f;
        GameObject.DontDestroyOnLoad(this.gameObject);
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {

        DicePlusConnector.Instance.registerListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (myDice != null && !refreshed)
        {
            myDice.subscribeOrientationReadouts(20);
            refreshed = true;
            StartCoroutine(refreshDelay());
        }

        Lean = Mathf.Lerp(Lean, leanReadout, 0.1f);

    }

    IEnumerator refreshDelay()
    {
        yield return new WaitForSeconds(2);
        refreshed = false;
    }

    #region IDicePlusConnectorListener implementation
    void IDicePlusConnectorListener.onNewDie(DicePlus dicePlus)
    {
    }

    void IDicePlusConnectorListener.onScanFinished(bool fail)
    {
    }

    void IDicePlusConnectorListener.onScanStarted()
    {
    }

    void IDicePlusConnectorListener.onConnectionLost(DicePlus dicePlus)
    {
    }

    void IDicePlusConnectorListener.onConnectionEstablished(DicePlus dicePlus)
    {
        dicePlus.initializePStorageCommunication();
        dicePlus.registerListener(this);
        dicePlus.subscribeOrientationReadouts(20);

        myDice = dicePlus;
        DiceConnected = true;
    }

    void IDicePlusConnectorListener.onConnectionFailed(DicePlus dicePlus, int errorCode, string excpMsg)
    {
    }

    void IDicePlusConnectorListener.onBluetoothStateChanged(DicePlusConnector.BluetoothState state)
    {
    }
    #endregion

    #region IDicePlusListener implementation
    public void onAccelerometerReadout(DicePlus dicePlus, long time, Vector3 v, int type, string errorMsg)
    {
    }

    public void onTapReadout(DicePlus dicePlus, long time, Vector3 v, string errorMsg)
    {
    }


    private float leanReadout = 0;
    public void onOrientationReadout(DicePlus dicePlus, long time, Vector3 v, string errorMsg)
    {
        if (firstReadOut)
        {
            startDirection = v;
            firstReadOut = false;
        }
        direction = v - startDirection;
        leanReadout = (float)direction.y / 90.0f;

    }

    public void onTemperatureReadout(DicePlus dicePlus, long time, float temperature, string errorMsg)
    {
    }

    public void onBatteryState(DicePlus dicePlus, DicePlusConnector.BatteryState state, int percentage, bool low, string errorMsg)
    {
    }

    public void onTouchReadout(DicePlus dicePlus, long time, int current, int change, string errorMsg)
    {
    }

    public void onProximityReadout(DicePlus dicePlus, long time, System.Collections.Generic.List<int> readouts, string errorMsg)
    {
    }

    public void onRunLedAnimationStatus(DicePlus dicePlus, string errorMsg)
    {
    }

    public void onRoll(DicePlus dicePlus, long time, int duration, int face, int invalidityFlags, string errorMsg)
    {

    }

    public void onFaceReadout(DicePlus dicePlus, long time, int face, string errorMsg)
    {
    }

    public void onMagnetometerReadout(DicePlus dicePlus, long time, Vector3 v, int type, string errorMsg)
    {
    }

    public void onLedState(DicePlus dicePlus, long time, DicePlusConnector.LedFace ledMask, long animationId, int type, string errorMsg)
    {
    }

    public void onSubscriptionChangeStatus(DicePlus dicePlus, DicePlusConnector.DataSource dataSource, string errorMsg)
    {
    }

    public void onSetModeStatus(DicePlus dicePlus, string errorMsg)
    {
    }

    public void onSleepStatus(DicePlus dicePlus, string errorMsg)
    {
    }

    public void onDiceStatistics(DicePlus dicePlus, DiceStatistics diceStatistics, string errorMsg)
    {
    }

    public void onPowerMode(DicePlus dicePlus, long time, DicePlusConnector.PowerMode mode, string errorMsg)
    {
    }

    public void onPStorageReset(DicePlus dicePlus, string errorMsg)
    {
    }

    public void onPStorageValueRead(DicePlus dicePlus, int handle, string str)
    {
    }

    public void onPStorageValueRead(DicePlus dicePlus, int handle, Vector3 vector)
    {
    }

    public void onPStorageValueRead(DicePlus dicePlus, int handle, int intvalue)
    {
    }

    public void onPStorageCommunicationInitialized(DicePlus dicePlus, int count)
    {
    }

    public void onPStorageOperationFailed(DicePlus dicePlus, string errorMsg)
    {
    }

    public void onPStorageValueWrite(DicePlus dicePlus, int handle)
    {
    }

    #endregion
}
