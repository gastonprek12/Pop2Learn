using UnityEngine;
using System.Collections;

public class LoginController : MonoBehaviour {

	public UIInput Username_Input;

	public UIInput Password_Input;

	public UIInput ForgotPass_Input;

	/// <summary>
	/// Method that gets called when the user presses Login
	/// </summary>
	public void Login()
	{
		// Grab the username
		string username = Username_Input.value;

		// Grab the password
		string password = Password_Input.value;

		// Do we need to do some previous validation??

		// Do login here against the API (And delete debug)
		Debug.Log(string.Format("user: {0}  pass: {1}", username, password));

		CorrectLogin ();

	}

	/// <summary>
	/// If the user is logged in
	/// </summary>
	void CorrectLogin()
	{
		// Change to the appropiate scene
		Application.LoadLevel("GameList");
	}

	// If the login failed
	void IncorrectLogin()
	{
		// Show some message?
	}

	// TODO:
	public void ForgotPassword()
	{
		// Grab the email to use to send the email of the forgot pass
		string emailToSendForgot = ForgotPass_Input.value;
	}

	// TODO:
	public void SignUp()
	{
		
	}
}
