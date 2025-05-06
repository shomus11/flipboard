using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowroomManager : MonoBehaviour
{
    [SerializeField] List<Sprite> backgroundList;
    [SerializeField] List<SpriteMask> SpriteMasks;
    [SerializeField] SpriteRenderer spriteRendererTarget;
    float baseAnimationDuration = .25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DoAnimationSequence();
        //DoAnimationSequence();
        //InvokeRepeating("ChangeBackground", 3f, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeBackground()
    {

        StartCoroutine(ChangeBackgroundCoroutine());
    }
    IEnumerator ChangeBackgroundCoroutine()
    {

        //DoAnimationSequence();
        //yield return new WaitForSeconds(3f);

        //RotateSpriteMask();

        //yield return new WaitForSeconds(.5f);

        //spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        //spriteRendererTarget.DOFade(1, .5f).From(0);
        //yield return new WaitForSeconds(.5f);
        yield return new WaitForSeconds(4f);
    }
    public void DoAnimationSequence()
    {
        Sequence sequence = DOTween.Sequence();
        spriteRendererTarget.DOFade(1, 0);
        float totalAnimationDuration = 3;
        //totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(0, baseAnimationDuration * 3f).From(1));
        totalAnimationDuration += baseAnimationDuration * 3f;
        Debug.Log($"{totalAnimationDuration}");

        totalAnimationDuration = RotateSpriteMaskDuration(totalAnimationDuration, sequence);
        sequence.InsertCallback(totalAnimationDuration, () =>
        {
            spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        });

        //totalAnimationDuration += baseAnimationDuration / .025f;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(1, baseAnimationDuration * 3f).From(0));
        totalAnimationDuration += baseAnimationDuration * 8f;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(0, baseAnimationDuration * 3f).From(1));
        totalAnimationDuration += baseAnimationDuration * 3f;
        sequence.Insert(totalAnimationDuration, Camera.main.DOFieldOfView(66, 1).From(57));
    }
    float RotateSpriteMaskDuration(float totalAnimationDuration, Sequence sequence)
    {
        for (int i = 0; i < SpriteMasks.Count; i++)
        {
            float random = Random.Range(0f, 1000);
            float delay = Random.Range(0f, baseAnimationDuration);
            float multiply = 0;
            if (random < 500)
            {
                multiply = 360f;
            }
            else
            {
                multiply = -360f;
            }
            sequence.Insert(totalAnimationDuration + delay, SpriteMasks[i].transform.DOLocalRotate(Vector3.right * multiply, baseAnimationDuration * 3f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear));
        }
        totalAnimationDuration += baseAnimationDuration * 4f;


        return totalAnimationDuration;
    }

    public void RotateSpriteMask()
    {
        for (int i = 0; i < SpriteMasks.Count; i++)
        {
            SpriteMasks[i].transform.DOLocalRotate(Vector3.right * 360, 1f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear);
        }
    }

}
