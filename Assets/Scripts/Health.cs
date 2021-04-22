using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [SerializeField] public int lives = 5;
    public static Health Instance { get; set; }

    void Start()
    {
        Instance = this;
    }

    public void UpdateHealth()
    {

        if (lives > numOfHearts)
        {
            lives = numOfHearts;
        }

        for(var i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
        UpdateHealth();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            GetDamage();
            lives--;
            Debug.Log("Player's HP: " + lives);
        }
        if (lives < 1)
        {
            Die();
        }
    }
}
