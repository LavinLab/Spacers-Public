using UnityEngine;

public static class Core
{
    public static float damage = 1f;
    public static bool complexShot = false;
    public static int killsBeforeHeal;



    public static void GetDamage()
    {
        if(PlayerPrefs.GetInt("ChosenShip") != 2 || PlayerPrefs.GetInt("FulminantStars") != 4)
        {
            complexShot = false;
        }
        if (PlayerPrefs.HasKey("ChosenShip"))
        {
            if (PlayerPrefs.GetInt("ChosenShip") == 0)
            {
                if (PlayerPrefs.GetInt("ClassicStars") == 0)
                {
                    damage = 1f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 1)
                {
                    damage = 2f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 2)
                {
                    damage = 2.5f;
                }
                else if (PlayerPrefs.GetInt("ClassicStars") == 3 || PlayerPrefs.GetInt("ClassicStars") == 4)
                {
                    damage = 3f;
                }
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 1)
            {
                if (PlayerPrefs.GetInt("ImmortalStars") == 0)
                {
                    damage = 3f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 1)
                {
                    damage = 3f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 2)
                {
                    damage = 4f;
                }
                else if (PlayerPrefs.GetInt("ImmortalStars") == 3 || PlayerPrefs.GetInt("ImmortalStars") == 4)
                {
                    damage = Random.Range(4, 7);
                    if (PlayerPrefs.GetInt("ImmortalStars") == 4)
                    {
                        killsBeforeHeal = 0;
                    }
                }

            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 2)
            {
                if (PlayerPrefs.GetInt("FulminantStars") == 0)
                {
                    damage = 5f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 1)
                {
                    damage = 5f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 2)
                {
                    damage = 6f;
                }
                else if (PlayerPrefs.GetInt("FulminantStars") == 3 || PlayerPrefs.GetInt("FulminantStars") == 4)
                {
                    damage = 6f;
                    if (PlayerPrefs.GetInt("FulminantStars") == 4)
                    {
                        complexShot = true;
                    }
                }
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 3)
            {
                damage = 5f;
            }
            else if (PlayerPrefs.GetInt("ChosenShip") == 4)
            {
                if(PlayerPrefs.GetInt("FirethrowerStars") == 0 || PlayerPrefs.GetInt("FirethrowerStars") == 1 || PlayerPrefs.GetInt("FirethrowerStars") == 2)
                {
                    damage = 8f;
                }
                else if(PlayerPrefs.GetInt("FirethrowerStars") == 3)
                {
                    damage = 9f;
                }
                else if(PlayerPrefs.GetInt("FirethrowerStars") == 4)
                {
                    damage = 10f;
                }
            }
        }
        else
        {
            damage = 1f;
        }
    }

    private static int GetLivesBasedOnStars(string starsKey)
    {
        if (PlayerPrefs.HasKey(starsKey))
        {
            int stars = PlayerPrefs.GetInt(starsKey);
            if (starsKey == "ClassicStars")
            {
                switch (stars)
                {
                    case 0:
                        return 3;
                    case 1:
                    case 2:
                        return 4;
                    case 3:
                    case 4:
                        return 5;
                    default:
                        return 3; // Handle unexpected values
                }
            }
            else if (starsKey == "ImmortalStars")
            {
                switch (stars)
                {
                    case 0:
                    case 1:
                        return 5;
                    case 2:
                        return 6;
                    case 3:
                    case 4:
                        return 8;
                    default:
                        return 5; // Handle unexpected values
                }
            }
            else if (starsKey == "FulminantStars")
            {
                switch (stars)
                {
                    case 0:
                        return 3;
                    case 1:
                        return 4;
                    case 2:
                        return 5;
                    case 3:
                    case 4:
                        return 6;
                    default:
                        return 3; // Handle unexpected values
                }
            }
            else if (starsKey == "FirethrowerStars")
            {
                switch (stars)
                {
                    case 0:
                        return 1;
                    case 1:
                    case 2:
                    case 3:
                        return 3;
                    case 4:
                        return 4;
                    default:
                        return 3; // Handle unexpected values
                }
            }
            else
            {
                return 3;
            }
        }
        else
        {
            return 3; // Default if stars not found
        }
    }
    
    public static int GetLives()
    {
        int lives = 3;

        if (PlayerPrefs.HasKey("ChosenShip"))
        {
            int chosenShip = PlayerPrefs.GetInt("ChosenShip");

            switch (chosenShip)
            {
                case 0:
                    lives = GetLivesBasedOnStars("ClassicStars");
                    break;
                case 1:
                    lives = GetLivesBasedOnStars("ImmortalStars");
                    break;
                case 2:
                    lives = GetLivesBasedOnStars("FulminantStars");
                    break;
                case 3:
                    lives = 5;
                    break;
                case 4:
                    lives = GetLivesBasedOnStars("FirethrowerStars");
                    break;
            }
        }

        if (PlayerPrefs.HasKey("HeartHave"))
        {
            lives += PlayerPrefs.GetInt("HeartHave");
        }
        return lives;
    }
}
