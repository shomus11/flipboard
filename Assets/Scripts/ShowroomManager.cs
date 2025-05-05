using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowroomManager : MonoBehaviour
{
    [SerializeField] List<Sprite> backgroundList;
    [SerializeField] List<SpriteMask> SpriteMasks;
    [SerializeField] SpriteRenderer spriteRendererTarget;

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
        StartCoroutine(ChangeBackgroundCoroutine());
    }
    IEnumerator ChangeBackgroundCoroutine()
    {
        yield return new WaitForSeconds(3f);

        RotateSpriteMask();
        spriteRendererTarget.DOFade(0, .5f).From(1);
        yield return new WaitForSeconds(.5f);

        spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        spriteRendererTarget.DOFade(1, .5f).From(0);
        yield return new WaitForSeconds(.5f);
    }
    public void RotateSpriteMask()
    {
        for (int i = 0; i < SpriteMasks.Count; i++)
        {
            SpriteMasks[i].transform.DOLocalRotate(Vector3.right * 360, 1f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear);
        }
    }
}
