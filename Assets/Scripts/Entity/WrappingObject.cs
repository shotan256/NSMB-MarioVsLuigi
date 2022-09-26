﻿using UnityEngine;

using Fusion;

public class WrappingObject : NetworkBehaviour {

    private Rigidbody2D body;
    private Vector2 width;
    private float min, max;

    public void Awake() {
        body = GetComponent<Rigidbody2D>();
        if (!body)
            body = GetComponentInParent<Rigidbody2D>();

        // null propagation is ok w/ GameManager.Instance
        if (!(GameManager.Instance?.loopingLevel ?? false)) {
            enabled = false;
            return;
        }

        min = GameManager.Instance.GetLevelMinX();
        max = GameManager.Instance.GetLevelMaxX();
        width = new(GameManager.Instance.levelWidthTile / 2f, 0);
    }

    public override void FixedUpdateNetwork() {
        if (body.position.x < min) {
            transform.position = body.position += width;
        } else if (body.position.x > max) {
            transform.position = body.position -= width;
        }
        body.centerOfMass = Vector2.zero;
    }
}
