using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour {
    public float startingPower = 100;
    public float currentPower;
    public Text powerText;
    public Image damageImage;
    public PowerColor powerColor;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    bool damaged;
    bool isDead;

    private void Awake()
    {
        currentPower = startingPower;
        UpdateText();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void GainPower(float amount)
    {
        currentPower += amount;
        powerColor.PowerUpdate(currentPower, startingPower);

        UpdateText();
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        currentPower -= amount;
        powerColor.PowerUpdate(currentPower,startingPower);

        UpdateText();

        if (currentPower <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
    }

    void UpdateText()
    {
        powerText.text = currentPower + "%";
    }
}

