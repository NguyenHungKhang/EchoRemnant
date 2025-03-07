using System.Collections.Generic;
using UnityEngine;

public class EchoController : MonoBehaviour
{
    public Transform player;
    public float delayTime = 0.5f;

    private Queue<Vector2> positionHistory = new Queue<Vector2>();
    private Queue<bool> facingHistory = new Queue<bool>();
    private Queue<Dictionary<string, object>> animationHistory = new Queue<Dictionary<string, object>>();

    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    private Animator echoAnimator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = player.GetComponent<Animator>();
        echoAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        positionHistory.Enqueue(player.position);
        facingHistory.Enqueue(player.localScale.x > 0);

        var parameters = new Dictionary<string, object>();
        foreach (var param in playerAnimator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    parameters[param.name] = playerAnimator.GetBool(param.name);
                    break;
                case AnimatorControllerParameterType.Float:
                    parameters[param.name] = playerAnimator.GetFloat(param.name);
                    break;
                case AnimatorControllerParameterType.Int:
                    parameters[param.name] = playerAnimator.GetInteger(param.name);
                    break;
            }
        }
        animationHistory.Enqueue(parameters);
        
        if (positionHistory.Count > delayTime / Time.deltaTime)
        {
            transform.position = positionHistory.Dequeue();
            bool facingRight = facingHistory.Dequeue();
            transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);

            var echoParameters = animationHistory.Dequeue();
            
            foreach (var param in echoParameters)
            {
                if (param.Value is bool boolValue && echoAnimator.GetBool(param.Key) != boolValue)
                    echoAnimator.SetBool(param.Key, boolValue);
                else if (param.Value is float floatValue && Mathf.Abs(echoAnimator.GetFloat(param.Key) - floatValue) > 0.01f)
                    echoAnimator.SetFloat(param.Key, floatValue);
                else if (param.Value is int intValue && echoAnimator.GetInteger(param.Key) != intValue)
                    echoAnimator.SetInteger(param.Key, intValue);
            }
        }
    }
}
