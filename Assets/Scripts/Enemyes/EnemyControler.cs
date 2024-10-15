using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public int Speed = 2;
    public Animator AnimatorEmeny;
    [Range(1, 99)]
    public int OfenStop = 70;

    private Vector2Int _sizeColiderNormal = new Vector2Int(15, 9);
    private Vector2Int _sizeColiderMove = new Vector2Int(22, 14);
    private int _playerLayer = 3;
    private int _targetIndex = 0;
    private int _targetIndexPatrul = 0;
    private Transform _player;
    private bool _canIGoToPlayer = false;
    private bool _canIGoToPatrul = false;
    private List<Node> _pathToPlayer = new();
    private List<Node> _pathToPatrul = new();
    private Pathfinding _pathF = new Pathfinding();

    private void OnDisable()
    {
        _pathToPatrul.Clear(); 
        _pathToPlayer.Clear();
        _canIGoToPlayer = false;
        _canIGoToPatrul = false;

    }
    public void Enter2D(Collider2D triger, Transform tr)
    {
        if (triger.transform.gameObject.layer == _playerLayer)
        {
            _player = triger.transform;
            tr.GetComponent<CapsuleCollider2D>().size = _sizeColiderMove;
            FindPathToPlayer(_player);

        }
    }
    private void FixedUpdate()
    {
        if (_canIGoToPlayer && _pathToPlayer != null && _pathToPlayer.Count > 0)
        {
            if (_targetIndex >= _pathToPlayer.Count)
            {
                FindPathToPlayer(_player);
            }
            else
            {
                MoveAlongPathToPlayer();
            }
            AnimatorRun();
        }
        else if (_pathToPatrul != null && _pathToPatrul.Count > 0)
        {

            AnimatorRun();
            if (_canIGoToPatrul)
            {

                if (_targetIndexPatrul > _pathToPatrul.Count)
                {
                    FindPatrulPath();
                }
                MoveAlongPathINPatrul();
            }
        }
        else
        {

            FindPatrulPath();
            AnimatorRun();
        }
    }

    public void Exit2D(Collider2D collision, Transform tr)
    {
        if (collision.transform.gameObject.layer == _playerLayer)
        {
            //pathF.target = null; 
            _canIGoToPlayer = false;
            tr.GetComponent<CapsuleCollider2D>().size = _sizeColiderNormal;
        }
    }
    void FindPathToPlayer(Transform player)
    {
        _pathF.FindPath(transform.position, _player.position);
        _pathToPlayer.Clear();
        if (_pathF.Path.Count > 24)
        {
            _canIGoToPlayer = false;
            TrigerEnemyCollider.TrigerCollider.GetComponent<CapsuleCollider2D>().size = _sizeColiderNormal;
        }
        else
        {
            _pathToPlayer.AddRange(_pathF.Path);
            _targetIndex = 0;
            _canIGoToPlayer = true;

            _targetIndexPatrul = 0;
            _pathToPatrul.Clear();
            _canIGoToPatrul = false;
        }
    }
    private void FindPatrulPath()
    {
        _pathToPatrul.Clear();
        Vector3 CurentPos = transform.position;
        List<Vector2Int> TargetsVectors = MapRendering.MainMap.FindAll(map =>
            Vector2.Distance(CurentPos, map) > Random.Range(2, 5) && Vector2.Distance(CurentPos, map) < 6);
        Vector2Int TargetPos = TargetsVectors[Random.Range(0, TargetsVectors.Count)];
        Vector3 t = new Vector3(TargetPos.x, TargetPos.y, 0);
        _pathF.FindPath(CurentPos, t);
        _pathToPatrul.AddRange(_pathF.Path);
        _targetIndexPatrul = 0;
        _canIGoToPatrul = true;
    }
    void MoveAlongPathToPlayer()
    {
        if (_pathToPlayer == null || _targetIndex >= _pathToPlayer.Count)
            return;

        Node targetNode = _pathToPlayer[_targetIndex];
        Vector3 targetPosition = targetNode.worldPosition;

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            FindPathToPlayer(_player);
        }
        else
        {
            ChangeTurnSprit(targetPosition);
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Speed * Time.deltaTime;

        }
    }
    void MoveAlongPathINPatrul()
    {
        _canIGoToPatrul = false;
        if (_pathToPatrul == null || _targetIndexPatrul >= _pathToPatrul.Count)
        {
            _targetIndexPatrul++;
            if (Random.Range(1, 101) > 70)
            {
                StartCoroutine(WaitwhenEnemyPatruls());
            }
            else
            {
                _canIGoToPatrul = true;
            }
        }
        else
        {
            Node targetNode = _pathToPatrul[_targetIndexPatrul];
            Vector3 targetPosition = targetNode.worldPosition;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                _targetIndexPatrul++;

            }
            else
            {
                ChangeTurnSprit(targetPosition);
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * Speed * Time.deltaTime;
            }
            _canIGoToPatrul = true;
        }
    }
    void ChangeTurnSprit(Vector3 targetStep)
    {
        if (transform.position.x > targetStep.x)
        {
            transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        }
    }
    IEnumerator WaitwhenEnemyPatruls()
    {
        _canIGoToPatrul = false;
        yield return new WaitForSeconds(Random.Range(5, 12));
        _canIGoToPatrul = true;
    }
    void AnimatorRun()
    {

        AnimatorEmeny.SetBool("RunBool", _canIGoToPlayer || _canIGoToPatrul);
    }


}


