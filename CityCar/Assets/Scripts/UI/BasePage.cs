using UnityEngine;

public class BasePage : MonoBehaviour {


    public virtual void PageInit()
    {

    }

    public virtual void GoPage(BasePage page)
    { 
        this.gameObject.SetActive(false);
        page.gameObject.SetActive(true);
    }

  
}
