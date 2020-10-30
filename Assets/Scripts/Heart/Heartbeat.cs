using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heartbeat : MonoBehaviour
{
    public static event Action OnBeat;

    public static Heartbeat Instance { get; private set; }

    [SerializeField] private Sprite commonSprite;
    [SerializeField] private Sprite transparentSprite;

    [SerializeField] private float scalePower;
    [SerializeField] private float standartNecessaryTouchTime;
    [SerializeField] private float force = 10f;
    [SerializeField] private float forceOffset = 0.1f;
    [SerializeField] private AudioClip sound;
    private float necessaryTouchTime;

    private float lastTouchTime;
    private bool counted;

    private new SpriteRenderer renderer;
    private HeartBlood blood;

    public event Action OnDown;
    public event Action OnDrag;
    public event Action OnUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        renderer = GetComponent<SpriteRenderer>();

        GameManager.OnStartGame += () => renderer.sprite = transparentSprite;
        GameManager.OnEndGame += () => renderer.sprite = commonSprite;
    }

    private void Start()
    {
        blood = HeartBlood.Instance;
    }

    private void OnEnable()
    {
        HormoneSpawner.OnSet += SetHormoneTouchTime;
        HormoneSpawner.OnRelease += SetIniTouchTime;
    }

    private void Update()
    {
        if (transform.localScale.x < 0.5f)
        {
            float necessaryScale = 1f * Time.deltaTime;
            transform.localScale += new Vector3(necessaryScale, necessaryScale, necessaryScale);
        }
    }

    private void OnMouseDown()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPause)
        {
            return;
        }

        lastTouchTime = 0;
        counted = false;
        OnDown?.Invoke();

        if (necessaryTouchTime < 0.07)
        {
            Beat();
            HandleBeat();
        }

        blood.Fill(true, 0.0002f / necessaryTouchTime * BPM.Instance?.Bpm ?? 60);
    }

    private void OnMouseDrag()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPause)
        {
            return;
        }

        if(StartButton.Instance != null)
        {
            StartButton.Instance.Drag();
        }

        HandleBeat();
        lastTouchTime += Time.deltaTime;
        OnDrag?.Invoke();

        if (!counted)
        {
            float scale = (scalePower / necessaryTouchTime + 1) * Time.deltaTime;
            transform.localScale -= new Vector3(scale, scale, scale);

            if (lastTouchTime > necessaryTouchTime)
                Beat();
        }

        blood.Fill(true, 0.0002f / necessaryTouchTime * BPM.Instance?.Bpm ?? 60);
    }

    private void OnMouseUp()
    {
        OnUp?.Invoke();
        blood.Fill(false, 0.0006f * BPM.Instance?.Bpm ?? 60);
    }

    private void Beat()
    {
        counted = true;
        SoundManager.Instance.PlaySound(sound);
        OnBeat?.Invoke();
        if (transform.localScale.x > scalePower)
            transform.localScale -= new Vector3(scalePower, scalePower, scalePower);
    }

    private void SetHormoneTouchTime(Hormone hormone)
    {
        necessaryTouchTime = hormone.data.necessaryTouchTime;
    }

    private void SetIniTouchTime()
    {
        necessaryTouchTime = standartNecessaryTouchTime;
    }

    private void OnDisable()
    {
        HormoneSpawner.OnSet -= SetHormoneTouchTime;
        HormoneSpawner.OnRelease -= SetIniTouchTime;
    }

    private void HandleBeat()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(inputRay, out RaycastHit hit))
        {
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        GameManager.Instance.StartGame();
    }
}
