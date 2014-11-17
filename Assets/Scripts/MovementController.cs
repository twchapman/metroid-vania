using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    // Physics
    public float
        speed_horizontal = 0f,
        speed_vertical = 0f,
        speed_horizontal_max = 3.0f,
        speed_veritcal_max = -5.0f,
        speed_horizontal_target = 0f,
        speed_vertical_target = 0f,
        acceleration = 0.1f,
        friction = 0.3f,
        gravity = 0.5f,
        speed_jump = 5.0f,
        extra_jump,
        extra_jump_max
        ;

    // States
    private enum DIRECTION {
        LEFT = -1,
        RIGHT = 1,
        UP = -1,
        DOWN = 1
    }
    private int facing = (int)DIRECTION.RIGHT;

    private bool
        grounded = true,
        jumping = false;

	void Start() {
        extra_jump_max = speed_jump;
        extra_jump = extra_jump_max;
	}
	
	void Update() {
        bool horizontal_input = HorizontalInput();
        bool vertical_input = VerticalInput();
        ApplyPhysics(horizontal_input, vertical_input);
	}

    private bool HorizontalInput() {
        float input_horizontal = Input.GetAxis("Horizontal");

        if(input_horizontal == 0) {
            speed_horizontal_target = 0;
            return false;
        }

        speed_horizontal_target = input_horizontal * speed_horizontal_max;
        facing = (int)(speed_horizontal_target < 0 ? DIRECTION.LEFT : DIRECTION.RIGHT);

        return true;
    }

    private bool VerticalInput() {
        float input_vertical = Input.GetAxis("Vertical");

        if (input_vertical <= 0) {
            extra_jump = extra_jump_max;
            jumping = false;
            return false;
        }
        
        if(!grounded) {
            if (!jumping || extra_jump <= 0f) {
                return false;
            }

            extra_jump -= gravity;
            speed_vertical += extra_jump * Time.deltaTime;
            return true;
        }

        grounded = false;
        jumping = true;

        speed_vertical = speed_jump;

        return true;
    }

    private void ApplyPhysics(bool horizontal_input, bool vertical_input) {
        float step_horizontal = horizontal_input ? acceleration : friction;
        step_horizontal *= Mathf.Sign(speed_horizontal_target) != Mathf.Sign(speed_horizontal) ? 2 : 1;
        speed_horizontal = Mathf.Lerp(speed_horizontal, speed_horizontal_target, step_horizontal);

        if(!horizontal_input && Mathf.Abs(speed_horizontal) < acceleration) speed_horizontal = 0;

        if(!grounded) {
            float step_vertical = gravity * Time.deltaTime;
            speed_vertical = Mathf.Lerp(speed_vertical, speed_veritcal_max, step_vertical);
        }

        Vector2 position = transform.position;
        position.x += speed_horizontal * Time.deltaTime;
        position.y += speed_vertical * Time.deltaTime;
        if(!grounded && position.y <= 0.32346f) {
            grounded = true;
            jumping = false;
            extra_jump = extra_jump_max;
            position.y = 0.32346f;
            speed_vertical = 0;
        } 
        transform.position = position;

        transform.localScale = new Vector2(facing, 1);
    }
}
