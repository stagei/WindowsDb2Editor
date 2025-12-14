using NLog;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Base class for DB2 completion data.
/// </summary>
public abstract class Db2CompletionDataBase : ICompletionData
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public string Text { get; set; } = string.Empty;
    public object Description { get; set; } = string.Empty;
    public double Priority { get; set; } = 1.0;
    public ImageSource? Image { get; set; }
    
    public object Content => Text;
    
    public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
    {
        Logger.Debug("Completing: {Text}", Text);
        textArea.Document.Replace(completionSegment, Text);
    }
}

/// <summary>
/// Completion data for SQL keywords.
/// </summary>
public class Db2KeywordCompletionData : Db2CompletionDataBase
{
    public Db2KeywordCompletionData()
    {
        // Keywords have highest priority
        Priority = 1.0;
    }
}

/// <summary>
/// Completion data for table names.
/// </summary>
public class Db2TableCompletionData : Db2CompletionDataBase
{
    public int RowCount { get; set; }
    
    public Db2TableCompletionData()
    {
        Priority = 2.0;
    }
    
    public new object Content
    {
        get
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(new TextBlock
            {
                Text = "📋 ",
                FontSize = 12,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = Text,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            if (RowCount > 0)
            {
                panel.Children.Add(new TextBlock
                {
                    Text = $" ({RowCount:N0} rows)",
                    FontSize = 10,
                    Foreground = Brushes.Gray,
                    Margin = new System.Windows.Thickness(5, 0, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                });
            }
            
            return panel;
        }
    }
}

/// <summary>
/// Completion data for view names.
/// </summary>
public class Db2ViewCompletionData : Db2CompletionDataBase
{
    public Db2ViewCompletionData()
    {
        Priority = 2.0;
    }
    
    public new object Content
    {
        get
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(new TextBlock
            {
                Text = "👁️ ",
                FontSize = 12,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = Text,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            return panel;
        }
    }
}

/// <summary>
/// Completion data for column names.
/// </summary>
public class Db2ColumnCompletionData : Db2CompletionDataBase
{
    public string ColumnName { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public bool IsNullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string TableName { get; set; } = string.Empty;
    
    public Db2ColumnCompletionData()
    {
        Priority = 3.0;
    }
    
    public new string Text => ColumnName;
    
    public new object Description => 
        $"{DataType}{(IsNullable ? "" : " NOT NULL")}{(IsPrimaryKey ? " PK" : "")} (from {TableName})";
    
    public new object Content
    {
        get
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            
            // Column name
            panel.Children.Add(new TextBlock
            {
                Text = ColumnName,
                FontWeight = IsPrimaryKey ? FontWeights.Bold : FontWeights.Normal,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            // Data type
            panel.Children.Add(new TextBlock
            {
                Text = $" : {DataType}",
                FontSize = 10,
                Foreground = Brushes.DarkBlue,
                Margin = new System.Windows.Thickness(5, 0, 0, 0),
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            // PK indicator
            if (IsPrimaryKey)
            {
                panel.Children.Add(new TextBlock
                {
                    Text = " 🔑",
                    FontSize = 10,
                    Margin = new System.Windows.Thickness(3, 0, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                });
            }
            
            return panel;
        }
    }
}

/// <summary>
/// Completion data for function names.
/// </summary>
public class Db2FunctionCompletionData : Db2CompletionDataBase
{
    public string? ParameterHint { get; set; }
    
    public Db2FunctionCompletionData()
    {
        Priority = 2.0;
    }
    
    public new object Content
    {
        get
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(new TextBlock
            {
                Text = "🔧 ",
                FontSize = 12,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = Text,
                FontWeight = FontWeights.SemiBold,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            if (!string.IsNullOrEmpty(ParameterHint))
            {
                panel.Children.Add(new TextBlock
                {
                    Text = $" ({ParameterHint})",
                    FontSize = 10,
                    Foreground = Brushes.Gray,
                    Margin = new System.Windows.Thickness(3, 0, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                });
            }
            
            return panel;
        }
    }
}

/// <summary>
/// Completion data for SQL snippets.
/// </summary>
public class Db2SnippetCompletionData : Db2CompletionDataBase
{
    public string Template { get; set; } = string.Empty;
    public string Trigger { get; set; } = string.Empty;
    
    public Db2SnippetCompletionData()
    {
        Priority = 1.5;
    }
    
    public new void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
    {
        // Replace trigger with template
        textArea.Document.Replace(completionSegment, Template);
        
        // Future: Add placeholder navigation with TAB
    }
    
    public new object Content
    {
        get
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(new TextBlock
            {
                Text = "📝 ",
                FontSize = 12,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = Trigger,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = $" ({Description})",
                FontSize = 10,
                Foreground = Brushes.DarkGreen,
                Margin = new System.Windows.Thickness(5, 0, 0, 0),
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            });
            
            return panel;
        }
    }
}

