using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class FuturoManager : MonoBehaviour
{
    public WorldManager wm;
    AudioSpectrum spectrum;

    public TrailRenderer[] fireFlies;

    public float fireFlyScale, wallColScale;
    float originalFireFlyScale, originalWallColScale;

    public NewPlayer p;

    public Material wallMat;

    Color c;

    float shortDelay;


    float[] spectrumValue, returnValue, scrollSpeed;


    public Mirror kaleidescope;
    float idleTimer, realInterval, subtractK;
    public float kInterval;
    //[HideInInspector]
    public bool kaleidescopeOn;

    public ParticleSystem roofPartycle;
    bool emittedRoof;
    float originalFOV, originalRoofPartyScale;
    public float roofPartyScale;

    public Transform partycles;
    public float partyScale;
    float originalPartyScale;

    public LineRenderer lightning;
    public float lightningScale;
    float originalLighningScale;

    public ParticleSystem TriParty;

    bool willNeedReset;

    Vector3 partyclesOriginalSize, lightningOriginalSize, lightningOriginalEuler, lightningOriginalPos;


    // Use this for initialization
    void Start()
    {

        originalFOV = Camera.main.fieldOfView;


        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        c = wallMat.color;

        this.enabled = false;

        wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * 0);

        scrollSpeed = new float[fireFlies.Length];


        for (int i = 0; i < fireFlies.Length; i++)
        {
            Keyframe[] kf = new Keyframe[10];
            for (int k = 0; k < kf.Length; k++)
            {
                kf[k] = new Keyframe(k / (kf.Length - 1f), 0.01f);

            }

            AnimationCurve curve = new AnimationCurve(kf);

            fireFlies[i].widthCurve = curve;
            lightning.widthCurve = curve;

            scrollSpeed[i] = i + 2;

        }

        spectrumValue = new float[fireFlies.Length];
        returnValue = new float[fireFlies.Length];


        partyclesOriginalSize = partycles.localScale;
        lightningOriginalSize = lightning.transform.localScale;
        lightningOriginalEuler = lightning.transform.localEulerAngles;
        lightningOriginalPos = lightning.transform.localPosition;

        originalFireFlyScale = fireFlyScale;
        originalWallColScale = wallColScale;
        originalRoofPartyScale = roofPartyScale;
        originalPartyScale = partyScale;
        originalLighningScale = lightningScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (p.inSong)
        {
            willNeedReset = true;
            if (shortDelay < 1.1f)
            {
                shortDelay += Time.deltaTime;//para saltarse ese segundito que se prenden las luces


            }
            else
            {

                //----------------------fireFly Trails----------------------

                for (int i = 0; i < fireFlies.Length; i++)//hacer que cada frame solo haga uno en realidad
                {


                    AnimationCurve curve = fireFlies[i].widthCurve;

                    for (int k = 1; k < curve.keys.Length - 1; k++)
                    {
                        curve.MoveKey(k, new Keyframe(curve.keys[k].time + Time.deltaTime / scrollSpeed[i],
                                                                        curve.keys[k].value));


                        if (curve.keys[k].time > 0.98f)
                        {
                            curve.RemoveKey(k);
                            Keyframe k0 = new Keyframe(0.01f, 0);
                            curve.AddKey(k0);
                            spectrumValue[i] = spectrum.MeanLevels[i % 10] * fireFlyScale;
                            returnValue[i] = curve.keys[curve.keys.Length - 2].value;

                        }



                    }

                    if (curve.keys[1].value < spectrumValue[i])
                    {
                        curve.MoveKey(1, new Keyframe(curve.keys[1].time,
                                                      curve.keys[1].value + (Time.deltaTime)));
                    }

                    if (curve.keys[curve.keys.Length - 2].value > 0 && curve.keys[curve.keys.Length - 2].time > 0.85f)
                    {

                        float speed = returnValue[i] * Time.deltaTime * 4;

                        curve.MoveKey(curve.keys.Length - 2, new Keyframe(curve.keys[curve.keys.Length - 2].time,
                                                                          curve.keys[curve.keys.Length - 2].value - speed));

                    }


                    curve.MoveKey(curve.keys.Length - 1, new Keyframe(1, 0));

                    //                    Debug.Log(curve.keys.Length);

                    fireFlies[i].widthCurve = curve;
                    fireFlies[i].widthMultiplier = Mathf.Lerp(fireFlies[i].widthMultiplier, spectrum.PeakLevels[i % 10] * fireFlyScale + 5, 0.25f);

                    fireFlies[i].widthMultiplier = Mathf.Clamp(fireFlies[i].widthMultiplier, 0, 80);

                }


                //----------------------wall Color----------------------

                wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * Mathf.Pow(spectrum.MeanLevels[1], 2) * wallColScale);


                //----------------------kaleidescope----------------------
                //                Debug.Log(p.mouseY);

                if (kaleidescopeOn)
                {
                    if (kaleidescope.enabled == false)
                        kaleidescope.enabled = true;
                }
                else
                {
                    if (kaleidescope.enabled == true)
                        kaleidescope.enabled = false;
                }

                if (kaleidescope.enabled == true)
                {

                    if (Mathf.Abs(p.mouseX) < 0.000005f && Mathf.Abs(p.mouseY) < 0.000005f)
                        idleTimer += Time.deltaTime;
                    else
                        idleTimer = 0;

                    //Debug.Log(idleTimer);

                    if (kaleidescope._repeat > 0)
                        realInterval = kInterval * kaleidescope._repeat;
                    else
                        realInterval = kInterval;


                    if (idleTimer > realInterval)
                    {
                        kaleidescope._repeat++;
                        //if (Camera.main.fieldOfView < 152)
                        //Camera.main.fieldOfView += 5;
                        subtractK = kaleidescope._repeat;
                        kaleidescope._symmetry = true;
                    }


                    if (idleTimer < kInterval - 1)
                    {
                        if (kaleidescope._repeat >= 0)
                        {
                            subtractK -= Time.deltaTime * 10f;

                            kaleidescope._repeat = Mathf.RoundToInt(subtractK);

                            //if (Camera.main.fieldOfView > originalFOV)
                            //Camera.main.fieldOfView -= Time.deltaTime * 100;
                        }
                        if (kaleidescope._repeat >= 1)
                            kaleidescope._symmetry = false;
                    }
                }

                //----------------------roofParticle----------------------

                if (spectrum.MeanLevels[7] > 0.002f * roofPartyScale)
                {
                    if (!emittedRoof)
                    {
                        //Debug.Log("KAS EMITS");
                        roofPartycle.Emit(1);
                        emittedRoof = true;
                    }
                }
                else
                {
                    emittedRoof = false;
                }

                //                Debug.Log(spectrum.MeanLevels[7]);

                //----------------------partycles----------------------

                partycles.localScale = Vector3.one * (spectrum.MeanLevels[4] * partyScale + 0.1f);

                //----------------------lightning----------------------

                AnimationCurve curve2 = lightning.widthCurve;

                for (int i = 0; i < lightning.positionCount; i++)
                {
                    float yPos = Mathf.Pow(spectrum.MeanLevels[i % 4] * lightningScale, i + 3) / lightningScale;
                    yPos = Mathf.Clamp(yPos, -10, 20);

                    lightning.SetPosition(i, Vector3.Lerp(lightning.GetPosition(i),
                                                          new Vector3(lightning.GetPosition(i).x, yPos, lightning.GetPosition(i).z),
                                                          0.1f));



                    curve2.MoveKey(i, new Keyframe(curve2.keys[i].time, Mathf.Lerp(curve2.keys[i].value,
                                                                                   spectrum.MeanLevels[i % 4] * lightningScale * 1.5f, 0.2f)));

                    curve2.MoveKey(curve2.keys.Length - 1, new Keyframe(1, curve2.keys[0].value));

                    lightning.widthCurve = curve2;
                    lightning.widthMultiplier = lightning.transform.localScale.y;

                }

                //----------------------Tris----------------------
                if (kaleidescope._repeat > 2)
                {
                    if (!TriParty.isPlaying)
                    {
                        TriParty.gameObject.SetActive(true);
                        TriParty.transform.parent = Camera.main.transform;
                        TriParty.transform.localPosition = new Vector3(0, 0, -1);
                        TriParty.transform.LookAt(Camera.main.transform.position);
                        TriParty.transform.localEulerAngles = new Vector3(0, 270, 0);
                        TriParty.Play();
                    }

                    // Debug.Log("playing:" + TriParty.isPlaying + "|| emitting:" + TriParty.isEmitting);
                }
                else
                {
                    TriParty.Stop();//y borrarles a todos!
                    TriParty.gameObject.SetActive(false);

                }
            }

        }
        else
        {
            if (willNeedReset)
            {
                ResetFuturo();
                willNeedReset = false;
            }
        }
    }

    void ResetFuturo()
    {
        for (int i = 0; i < fireFlies.Length; i++)
        {
            Keyframe[] kf = new Keyframe[10];
            for (int k = 0; k < kf.Length; k++)
            {
                kf[k] = new Keyframe(k / (kf.Length - 1f), 0.01f);


            }

            AnimationCurve curve = new AnimationCurve(kf);

            fireFlies[i].widthCurve = curve;
            lightning.widthCurve = curve;

            scrollSpeed[i] = i + 2;

        }
        kaleidescope._repeat = 0;
        kaleidescope._symmetry = false;
        kaleidescope.enabled = false;

        wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * 0);
        shortDelay = 0;

        partycles.localScale = partyclesOriginalSize;
        lightning.transform.localScale = lightningOriginalSize;
        lightning.transform.localEulerAngles = lightningOriginalEuler;
        lightning.transform.localPosition = lightningOriginalPos;

        TriParty.Stop();
        TriParty.transform.parent = transform.parent;
        TriParty.gameObject.SetActive(false);

    }

    public void adjustScales()
    {

        fireFlyScale = originalFireFlyScale * wm.masterScaleSpectrum;
        wallColScale = originalWallColScale * wm.masterScaleSpectrum;
        roofPartyScale = originalRoofPartyScale * wm.masterScaleSpectrum;
        partyScale = originalPartyScale * wm.masterScaleSpectrum;
        lightningScale = originalLighningScale * wm.masterScaleSpectrum;



    }
}
