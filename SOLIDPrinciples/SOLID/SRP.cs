using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace SOLIDPrinciples.SOLID.SRP
{
  // just stores a couple of journal entries and ways of working with them
  public class Journal
  {
    private readonly List<string> entries = new List<string>();

    private static int count = 0;

    public int AddEntry(string text)
    {
      entries.Add($"{++count}: {text}");
      return count; // memento pattern!
    }

    public void RemoveEntry(int index)
    {
      entries.RemoveAt(index);
    }

    public override string ToString()
    {
      return string.Join(Environment.NewLine, entries);
    }

    // breaks single responsibility principle
    public void Save(string filename, bool overwrite = false)
    {
      File.WriteAllText(filename, ToString());
    }
  }

  // handles the responsibility of persisting objects
  public class PersistenceManager
  {
    public void SaveToFile(Journal journal, string filename, bool overwrite = false)
    {
      if (overwrite || !File.Exists(filename))
        File.WriteAllText(filename, journal.ToString());
    }
  }

    //This principle should have only one single reason to change
  public class SRP
  {
    public void Run()
    {
      var journal = new Journal();
      journal.AddEntry("Hello");
      journal.AddEntry("World");
      WriteLine(journal);

      var persistance = new PersistenceManager();
      var filename = @"c:\temp\journal.txt";
      persistance.SaveToFile(journal, filename);
      Process.Start(filename);
    }
  }
}
