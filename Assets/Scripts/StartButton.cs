using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
   [SerializeField]
   private Animator animator;

   private void Start(){
      StartAnimation();
   }

   public void StartTransition(Animator anim){
      anim.SetTrigger("Fade");
   }

   public void StartGame(){
      if (GameManager.Instance != null)
         GameManager.Instance.NewGame();
      SceneManager.LoadScene("1-1");
   }

   private void StartAnimation(){
      animator.SetTrigger("Fade");
   }
}
