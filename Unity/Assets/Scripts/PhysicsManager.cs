using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    void Start()
    {
        // Find all GameObjects with MeshRenderers in the scene
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            // Check if the GameObject already has a Rigidbody component
            Rigidbody rb = meshRenderer.GetComponent<Rigidbody>();
            if (rb == null)
            {
                // Add a Rigidbody component
                rb = meshRenderer.gameObject.AddComponent<Rigidbody>();
                // Configure the Rigidbody properties as needed
                rb.mass = 1.0f;
                rb.drag = 0.0f;
                rb.angularDrag = 0.05f;
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            }

            // Disable gravity for the Rigidbody


            // Check if the GameObject already has a MeshCollider component
            if (meshRenderer.GetComponent<MeshCollider>() == null)
            {
                // Add a MeshCollider component
                MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>();
                // Configure the MeshCollider properties as needed
                meshCollider.convex = false; // Enable convex for dynamic objects
                meshCollider.isTrigger = false; // Set to true if you want it to be a trigger collider
            }
        }
    }
}