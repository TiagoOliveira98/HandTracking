T = readtable('../DataLogging/VelocityJoint/Logs(1634851576)_Speed.csv','NumHeaderLines',1);

MyFolderInfo = dir('../DataLogging/VelocityJoint/*.csv')

for file = 1:size(MyFolderInfo,1)

    fileName = MyFolderInfo(file).name
    
    Data = table2array(T);
    x = Data(:,1);
    Time = Data(:,43);
    
    
    filterX = zeros(29,1)
    filterT = zeros(29,1)
    
    %figure(1)
    fh = figure('Name', sprintf('Right Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 1:21
        x = Data(:,joint);
        it = 1
        for i = 1:size(a,1)
            if x(i,1) ~= 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end
      
        nexttile
        plot(filterT,filterX,'-o')
        ylim([0 100])
        title(sprintf('Plot of the variance of mean Velocity in the interval [t-0.02 t] of the Joint%i of the Right Hand', t))
        ylabel('Mean Velocity') 
        xlabel('Final Timestamp in Interval (t) [t-0.02 t]') 
        legend({'Mean Velocity'},'Location','northwest')
        ax = gca;
        ax.FontSize = 5;
    end
    
    %figure(2)
    fh = figure('Name', sprintf('Left Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for t = 22:42
        x = Data(:,t);
        it = 1
        for i = 1:size(a,1)
            if x(i,1) ~= 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end
        
        nexttile
        plot(filterT,filterX,'-o')
        ylim([0 100])
        title(sprintf('Plot of the variance of mean Velocity in the interval [t-0.02 t] of the Joint%i of the Left Hand', t-21))
        ylabel('Mean Velocity') 
        xlabel('Final Timestamp in Interval (t) [t-0.02 t]') 
        legend({'Mean Velocity'},'Location','northwest')
        ax = gca;
        ax.FontSize = 5;
    end
end