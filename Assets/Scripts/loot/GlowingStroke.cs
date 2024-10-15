using UnityEngine; 

public class GlowingStroke : MonoBehaviour
{
    public GameObject MainObject;
    public int TimerStart = 30;
    public Color color;
    public float valueMax;
    public float speedChangeProcent = 1f;
    public float speedUpDown = 0.01f;
    public float MaxDistanceUpDown = 1f;

    private Vector3 _minSize = new();
    private Vector3 _maxSize = new();
    private bool _maxMinScale = false;
    private bool _maxMinYSpace = false;
    private GameObject _parent;
    private float _yMinPos;
    private float _yMaxPos;
    private bool _firstTime = true;
    private int _timerStupUp = 15;
    private int _timerStupDown = 15;
    private Vector3 ValueChangeSize = new();


    void Start()
    {
        TimerStart = Random.Range(TimerStart - 10, TimerStart + 20);
        _parent = transform.parent.gameObject;
        transform.localScale = MainObject.transform.localScale;
        GetComponent<SpriteRenderer>().sprite = MainObject.GetComponent<SpriteRenderer>().sprite;
        MainObject.GetComponent<SpriteRenderer>().color = color;
        float value = (MainObject.transform.localScale.x / 100) * speedChangeProcent;
        ValueChangeSize = new Vector3(value, value, 0);

        _minSize = transform.localScale;
        _maxSize = transform.localScale * valueMax;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimerStart < 0)
        {
            if (_firstTime)
            {
                _yMinPos = _parent.transform.localPosition.y - (MaxDistanceUpDown / 2);
                _yMaxPos = _parent.transform.localPosition.y + (MaxDistanceUpDown / 2);
                _firstTime = false;
            }
            if (!_maxMinScale && MainObject.transform.localScale.x < _maxSize.x)
            {
                MainObject.transform.localScale += ValueChangeSize;
            }
            else
            {
                _maxMinScale = true;
            }

            if (_maxMinScale && MainObject.transform.localScale.x > _minSize.x)
            {
                MainObject.transform.localScale -= ValueChangeSize;
            }
            else
            {
                _maxMinScale = false;
            }

            UpDown();
        }
        else
        {
            TimerStart--;
        }
    }
    void UpDown()
    {

        if (!_maxMinYSpace && _parent.transform.localPosition.y < _yMaxPos)
        {
            _parent.transform.localPosition += new Vector3(0, speedUpDown, 0);
        }
        else
        {
            if (_timerStupUp < 0)
            {
                _maxMinYSpace = true;
                _timerStupUp = 15;
            }
            else
            {
                _timerStupUp--;
            }
        }

        if (_maxMinYSpace && _parent.transform.localPosition.y > _yMinPos)
        {
            _parent.transform.localPosition -= new Vector3(0, speedUpDown, 0);
        }
        else
        {
            if (_timerStupDown < 0)
            {
                _maxMinYSpace = false;
                _timerStupDown = 15;
            }
            else
            {
                _timerStupDown--;
            }
        }
    }
}
