using UnityEngine;

public class Selector : Node
{
    public Selector(string name) : base(name) { }

    public override Status Process()
    {
        Debug.Log("in selector " + name);

        for (int i = 0; i < children.Count; i++)
        {
            var status = children[i].Process();

            switch (status)
            {
                case Status.Running:
                    return Status.Running;

                case Status.Success:
                    return Status.Success;

                case Status.Failure:
                    continue;
            }
        }

        return Status.Failure;
    }
}
