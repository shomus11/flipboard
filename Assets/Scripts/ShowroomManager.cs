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
        InvokeRepeating("ChangeBackground", 3f, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeBackground()
    {
        DoAnimationSequence();
        //StartCoroutine(ChangeBackgroundCoroutine());
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
        float totalAnimationDuration = 0;
        totalAnimationDuration+= baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(0, baseAnimationDuration*3f).From(1));
        totalAnimationDuration += baseAnimationDuration * 3f;
        totalAnimationDuration = RotateSpriteMaskDuration(totalAnimationDuration, sequence);
        totalAnimationDuration += baseAnimationDuration * 3f;
        sequence.InsertCallback(totalAnimationDuration, () =>
        {
            spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        });
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(1, baseAnimationDuration * 3f).From(0));

    }
    float RotateSpriteMaskDuration(float totalAnimationDuration,Sequence sequence)
    {

        for (int i = 0; i < SpriteMasks.Count; i++)
        {
            float delay = Random.Range(0f, baseAnimationDuration);
            sequence.Insert(totalAnimationDuration + delay, SpriteMasks[i].transform.DOLocalRotate(Vector3.right * 360, baseAnimationDuration*3f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear));
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
