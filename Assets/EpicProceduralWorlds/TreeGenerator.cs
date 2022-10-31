using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] GameObject branch;

    [SerializeField] int maxLevels = 12;
    [SerializeField] float initialSize = 2f;
    [SerializeField, Range(0, 1)]  float reductionPerLevel = 0.234f;

    int myCurrentLevel = 1;
    Queue<GameObject> myRootBranchesQueue = new Queue<GameObject>();

    private void Start()
    {
        GameObject rootBranch = Instantiate(branch, transform);

        ModifyMyBranchesSizes(rootBranch, initialSize);

        myRootBranchesQueue.Enqueue(rootBranch);
        GenerateDaEpicTree();
    }
    void GenerateDaEpicTree()
    {
        if (myCurrentLevel >= maxLevels) return;
        ++myCurrentLevel;//prefix increment

        float updatedSize = Mathf.Max(initialSize - initialSize * reductionPerLevel * (myCurrentLevel - 1), 0.101f);

        var myBranchesCreatedThisCycle = new List<GameObject>();

        while (myRootBranchesQueue.Count > 0 && true)
        {
            var baseBranch = myRootBranchesQueue.Dequeue();
            //var baseBranch = myRootBranchesQueue.Queue();

            //var LBranch = GenerateMyBranches(baseBranch, -Random.Range(6f, 22f));
            //var RBranch = GenerateMyBranches(baseBranch, -Random.Range(6f, 22f));
            var LBranch = GenerateMyBranches(baseBranch, Random.Range(5f, 25f));

            var RBranch = GenerateMyBranches(baseBranch, -Random.Range(5f, 25f));

            ModifyMyBranchesSizes(LBranch, updatedSize);
            ModifyMyBranchesSizes(RBranch, updatedSize);

            myBranchesCreatedThisCycle.Add(LBranch);
            myBranchesCreatedThisCycle.Add(RBranch);
        }
        foreach (var updatedNewBranches in myBranchesCreatedThisCycle) myRootBranchesQueue.Enqueue(updatedNewBranches);
        GenerateDaEpicTree();
    }
    private GameObject GenerateMyBranches(GameObject previousBranch, float relativeAngle)
    {
        GameObject myNewGeneratedBranch = Instantiate(branch, transform);
        myNewGeneratedBranch.transform.localPosition = previousBranch.transform.localPosition + previousBranch.transform.up * GetMyBranchLength(previousBranch);

        //myNewGeneratedBranch.transform.localRotation = previousBranch.transform.localRotation * Quaternion.Euler(2, 2, relativeAngle);
        myNewGeneratedBranch.transform.localRotation = previousBranch.transform.localRotation * Quaternion.Euler(0, 0, relativeAngle);

        return myNewGeneratedBranch;
    }

    void ModifyMyBranchesSizes(GameObject branchInstance, float newSize)
    {
        var square = branchInstance.transform.GetChild(0);
        //var square = branchInstance.transform.GetChild(2);
        var circle = branchInstance.transform.GetChild(1);

        var newScale = square.transform.localScale;
        newScale.y = newSize;
        square.transform.localScale = newScale;

        var newPosition = square.transform.localPosition;
        //var newPosition = square.transform;
        newPosition.y = newSize / 2;
        //square.transform.localPosition.Position = newPosition;
        square.transform.localPosition = newPosition;



        var newCirclePosition = circle.transform.localPosition;
        newCirclePosition.y = newSize;
        circle.transform.localPosition = newCirclePosition;
    }

    float GetMyBranchLength(GameObject currentBranchInstance)
    {
        return currentBranchInstance.transform.GetChild(0).localScale.y;
        //return currentBranchInstance.transform.localScale.y; //need to access the child first
    }
}
