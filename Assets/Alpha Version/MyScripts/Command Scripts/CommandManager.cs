using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandManager : MonoSingleton<CommandManager>
{
    private List<ICommand> _commandBuffer = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        ///To store a command in a list, every time the command was done. This is to implement the Ctrl+Z functionality.
        _commandBuffer.Add(command);
    }

    /*public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");
        foreach (var command in _commandBuffer)
        {
            command.Execute();
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Finished");
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        foreach (var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undo();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Done()
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag("TestTag");

        foreach (var obj in objectsWithTag)
        {
            Material mat = obj.GetComponent<MeshRenderer>().material;
            mat.color = Color.white;
        }
    }*/

    public void ClearCommandBuffer()
    {
        _commandBuffer.Clear();
    }
}
